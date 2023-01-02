using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vue_expenses_api.Domain;
using vue_expenses_api.Infrastructure.Security;

namespace vue_expenses_api.Infrastructure;

public class DataSeeder
{
    public static async Task SeedData(
        ExpensesContext context,
        IPasswordHasher passwordHasher)
    {
        if (!context.Users.Any())
        {
            var salt = Guid.NewGuid().ToByteArray();
            var user = new User(
                "John",
                "Doe",
                "test@demo.com",
                passwordHasher.Hash(
                    "test",
                    salt),
                salt
            )
            {
                UseDarkMode = true
            };

            await context.Users.AddAsync(user);
                
            var expenseCategories = new List<ExpenseCategory>
            {
                new(
                    "General Expenses",
                    string.Empty,
                    2000m,
                    "#CE93D8",
                    user),
                new(
                    "Shopping",
                    string.Empty,
                    3000m,
                    "#64B5F6",
                    user),
                new(
                    "Utilities",
                    string.Empty,
                    2500m,
                    "#26A69A",
                    user),
                new(
                    "Travel",
                    string.Empty,
                    1000m,
                    "#FB8C00",
                    user)
            };

            await context.ExpenseCategories.AddRangeAsync(expenseCategories);

            var expenseTypes = new List<ExpenseType>
            {
                new(
                    "Credit Card",
                    string.Empty,
                    user),
                new(
                    "Debit Card",
                    string.Empty,
                    user),
                new(
                    "Cheque",
                    string.Empty,
                    user),
                new(
                    "Cash",
                    string.Empty,
                    user)
            };

            await context.ExpenseTypes.AddRangeAsync(expenseTypes);

            var r = new Random();
            //create expenses for a few years
            for (var i = 1; i <= 12; i++)
            {
                for (int j = 2019; j <= 2022; j++)
                {
                    var dates = Enumerable.Range(
                        1,
                        28).ToList();

                    var expense1 = new Expense(
                        new DateTime(
                            j,
                            i,
                            dates[r.Next(
                                1,
                                28)]),
                        expenseCategories.ToList()[r.Next(
                            0,
                            3)],
                        expenseTypes.ToList()[r.Next(
                            0,
                            3)],
                        (decimal) (r.NextDouble() * 1500),
                        string.Empty,
                        user
                    );

                    var expense2 = new Expense(
                        new DateTime(
                            j,
                            i,
                            dates[r.Next(
                                1,
                                28)]),
                        expenseCategories.ToList()[r.Next(
                            0,
                            3)],
                        expenseTypes.ToList()[r.Next(
                            0,
                            3)],
                        (decimal) (r.NextDouble() * 1500),
                        string.Empty,
                        user
                    );

                    await context.Expenses.AddRangeAsync(
                        expense1,
                        expense2);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}