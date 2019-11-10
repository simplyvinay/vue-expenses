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
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
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
            var user = new User(
                "John",
                "Doe",
                "test@demo.com",
                new PasswordHasher().Hash(
                    "test",
                    salt),
                salt
            )
            {
                Id = 1,
                UseDarkMode = true,
                CreatedAt = createdAt,
                UpdatedAt = createdAt
            };
            modelBuilder.Entity<User>().HasData(user);

            modelBuilder.Entity<ExpenseCategory>().HasData(
                new
                {
                    Id = 1,
                    Name = "General Expenses",
                    Budget = 0m,
                    Colour = "",
                    CreatedAt = createdAt,
                    UpdatedAt = createdAt,
                    UserId = user.Id
                }
            );
            modelBuilder.Entity<ExpenseType>().HasData(
                new
                {
                    Id = 1,
                    Name = "Credit Card",
                    CreatedAt = createdAt,
                    UpdatedAt = createdAt,
                    UserId = user.Id
                }
            );
            modelBuilder.Entity<Expense>().HasData(
                new
                {
                    Id = 1,
                    Date = DateTime.Now,
                    CategoryId = 1,
                    TypeId = 1,
                    Value = 10m,
                    CreatedAt = createdAt,
                    UpdatedAt = createdAt,
                    UserId = user.Id
                }
            );

            #endregion
        }
    }
}