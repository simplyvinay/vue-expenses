using System;
using vue_expense_api.Infrastructure;

namespace vue_expense_api.Domain
{
    public class Expense : Entity
    {
        public Expense(
            DateTime date,
            ExpenseCategory category,
            ExpenseType type,
            decimal value,
            string comments)
        {
            Date = date;
            Category = category;
            Type = type;
            Value = value;
            Comments = comments;
        }

        public DateTime Date { get; }
        public ExpenseCategory Category { get; }
        public ExpenseType Type { get; }
        public decimal Value { get; }
        public string Comments { get; }
    }
}