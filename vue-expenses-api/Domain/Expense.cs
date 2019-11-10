using System;
using vue_expenses_api.Infrastructure;

namespace vue_expenses_api.Domain
{
    public class Expense : Entity
    {
        protected Expense()
        {
        }

        public Expense(
            DateTime date,
            ExpenseCategory category,
            ExpenseType type,
            decimal value,
            string comments,
            User user)
        {
            Date = date;
            Category = category;
            Type = type;
            Value = value;
            Comments = comments;
            User = user;
        }

        public DateTime Date { get; set; }
        public ExpenseCategory Category { get; set; }
        public ExpenseType Type { get; set; }
        public decimal Value { get; set; }
        public string Comments { get; set; }
        public User User { get; set; }
    }
}