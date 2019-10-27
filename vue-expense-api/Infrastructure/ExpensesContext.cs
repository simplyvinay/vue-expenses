using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using vue_expense_api.Domain;
using vue_expense_api.Infrastructure.Security;

namespace vue_expense_api.Infrastructure
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
        }
    }
}