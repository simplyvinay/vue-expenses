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
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher _passwordHasher;

        public ExpensesContext(
            DbContextOptions options,
            IConfiguration configuration,
            IPasswordHasher passwordHasher) : base(options)
        {
            _configuration = configuration;
            _passwordHasher = passwordHasher;
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
            
            #region Seed

            var createdAt = DateTime.Now;
            var salt = Guid.NewGuid().ToByteArray();
            var user = new User(
                "John",
                "Doe",
                "test@demo.com",
                _passwordHasher.Hash(
                    "test",
                    salt),
                salt
            )
            {
                Id = 1,
                UseDarkMode = true,
                CreatedAt = createdAt,
                UpdatedAt = createdAt,
                Archived = false
            };
            modelBuilder.Entity<User>().HasData(user);


            modelBuilder.Entity<ExpenseCategory>().HasData(
                new
                {
                    Id = 1,
                    Name = "General Expenses",
                    Budget = 2000m,
                    ColourHex = "#CE93D8",
                    CreatedAt = createdAt,
                    UpdatedAt = createdAt,
                    UserId = user.Id,
                    Archived = false
                },
                new
                {
                    Id = 2,
                    Name = "Shopping",
                    Budget = 3000m,
                    ColourHex = "#64B5F6",
                    CreatedAt = createdAt,
                    UpdatedAt = createdAt,
                    UserId = user.Id,
                    Archived = false
                },
                new
                {
                    Id = 3,
                    Name = "Utilities",
                    Budget = 2500m,
                    ColourHex = "#26A69A",
                    CreatedAt = createdAt,
                    UpdatedAt = createdAt,
                    UserId = user.Id,
                    Archived = false
                },
                new
                {
                    Id = 4,
                    Name = "Travel",
                    Budget = 1000m,
                    ColourHex = "#FB8C00",
                    CreatedAt = createdAt,
                    UpdatedAt = createdAt,
                    UserId = user.Id,
                    Archived = false
                }

            );
            modelBuilder.Entity<ExpenseType>().HasData(
                new
                {
                    Id = 1,
                    Name = "Credit Card",
                    CreatedAt = createdAt,
                    UpdatedAt = createdAt,
                    UserId = user.Id,
                    Archived = false
                },
                new
                {
                    Id = 2,
                    Name = "Debit Card",
                    CreatedAt = createdAt,
                    UpdatedAt = createdAt,
                    UserId = user.Id,
                    Archived = false
                },
                new
                {
                    Id = 3,
                    Name = "Cheque",
                    CreatedAt = createdAt,
                    UpdatedAt = createdAt,
                    UserId = user.Id,
                    Archived = false
                },
                
                new
                {
                    Id = 4,
                    Name = "Cash",
                    CreatedAt = createdAt,
                    UpdatedAt = createdAt,
                    UserId = user.Id,
                    Archived = false
                }
            );

            var r = new Random();
            var expenseId = 0;
            //create expenses for a few years
            for (var i = 1; i <= 12; i++)
            {
                int[] indexes = {1, 2, 3, 4};
                var dates = Enumerable.Range(
                    1,
                    28).ToList();

                modelBuilder.Entity<Expense>().HasData(
                    new
                    {
                        Id = ++expenseId,
                        Date = new DateTime(
                            2019,
                            i,
                            dates[r.Next(1, 28)]),
                        CategoryId = indexes[r.Next(1, 4)],
                        TypeId = indexes[r.Next(1, 4)],
                        Value = (decimal)(r.NextDouble() * 1500),
                        CreatedAt = new DateTime(
                            2019,
                            i,
                            dates[r.Next(1, 28)]),
                        UpdatedAt = new DateTime(
                            2019,
                            i,
                            dates[r.Next(1, 28)]),
                        UserId = user.Id,
                        Archived = false
                    },
                    new
                    {
                        Id = ++expenseId,
                        Date = new DateTime(
                            2019,
                            i,
                            dates[r.Next(1, 28)]),
                        CategoryId = indexes[r.Next(1, 4)],
                        TypeId = indexes[r.Next(1, 4)],
                        Value = (decimal)(r.NextDouble() * 1500),
                        CreatedAt = new DateTime(
                            2019,
                            i,
                            dates[r.Next(1, 28)]),
                        UpdatedAt = new DateTime(
                            2019,
                            i,
                            dates[r.Next(1, 28)]),
                        UserId = user.Id,
                        Archived = false
                    });
            }

            #endregion
        }
    }
}