using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using vue_expenses_api.Domain;
using vue_expenses_api.Infrastructure.Security;

namespace vue_expenses_api.Infrastructure
{
    public class ExpensesContext : DbContext
    {
        private IConfiguration _configuration;

        public ExpensesContext(
            DbContextOptions options,
            IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseCategory> ExpenseCateogries { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));
        }

        public override Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = new CancellationToken())
        {
            UpdateAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        private void UpdateAuditFields()
        {
            // get entries that are being Added or Updated
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                var entity = entry.Entity as Entity;
                var now = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = now;
                }

                entity.UpdatedAt = now;
            }
        }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>()
                .HasOne(d => d.Category)
                .WithMany(e => e.Expenses);

            modelBuilder.Entity<Expense>()
                .HasOne(d => d.Type)
                .WithMany(e => e.Expenses);

            #region Seed
            var createdAt = DateTime.Now;
            var salt = Guid.NewGuid().ToByteArray();
            modelBuilder.Entity<User>().HasData(
                new User(
                    "test",
                    "foo@bar.com",
                    new PasswordHasher().Hash(
                        "test",
                        salt),
                    salt
                )
                {
                    Id = 1,
                    CreatedAt = createdAt,
                    UpdatedAt = createdAt
                });

            var expenseCategory = new ExpenseCategory(
                "General Expenses",
                "")
            {
                Id = 1,
                CreatedAt = createdAt,
                UpdatedAt = createdAt
            };
            var expenseType = new ExpenseType(
                "Credit Card",
                "")
            {
                Id = 1,
                CreatedAt = createdAt,
                UpdatedAt = createdAt
            };

            modelBuilder.Entity<ExpenseCategory>().HasData(
                expenseCategory
            );
            modelBuilder.Entity<ExpenseType>().HasData(
                expenseType
            );
            modelBuilder.Entity<Expense>().HasData(
                new
                {
                    Id = 1,
                    Date = DateTime.Now,
                    CategoryId = expenseCategory.Id,
                    TypeId = expenseType.Id,
                    Value = 10m,
                    CreatedAt = createdAt,
                    UpdatedAt = createdAt
                }
            ); 
            #endregion
        }
    }
}