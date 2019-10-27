using vue_expense_api.Infrastructure;

namespace vue_expense_api.Domain
{
    public class ExpenseType : Entity
    {
        public ExpenseType(
            string name,
            string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; }
        public string Description { get; }
    }
}