using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using vue_expenses_api.Domain;

namespace vue_expenses_api.Infrastructure
{
    public class ExpensesContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly ILoggerFactory _loggerFactory;

        public ExpensesContext(
            DbContextOptions options,
            IConfiguration configuration,
            ILoggerFactory loggerFactory) : base(options)
        {
            _configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));
            optionsBuilder.UseLoggerFactory(_loggerFactory);
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
            var navigation = modelBuilder.Entity<User>()
                .Metadata.FindNavigation(nameof(User.RefreshTokens));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Entity<RefreshToken>()
                .HasOne(d => d.User)
                .WithMany(e => e.RefreshTokens)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);;

            modelBuilder.Entity<Expense>()
                .HasOne(d => d.Category)
                .WithMany(e => e.Expenses);

            modelBuilder.Entity<Expense>()
                .HasOne(d => d.Type)
                .WithMany(e => e.Expenses);
        }
    }
}