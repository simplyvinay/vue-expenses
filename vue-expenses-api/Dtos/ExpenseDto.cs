using System;

namespace vue_expenses_api.Dtos
{
    public class ExpenseDto
    {
        protected ExpenseDto()
        {
        }

        public ExpenseDto(
            int id,
            string date,
            int categoryId,
            string categoryName,
            decimal categoryBudget,
            string categoryColour,
            int typeId,
            string typeName,
            decimal value,
            string comments)
        {
            Id = id;
            Date = date;
            CategoryId = categoryId;
            Category = categoryName;
            CategoryBudget = categoryBudget;
            CategoryColour = categoryColour;
            TypeId = typeId;
            Type = typeName;
            Value = value;
            Comments = comments;
        }

        public int Id { get; set; }
        public string Date { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public decimal CategoryBudget { get; set; }
        public string CategoryColour { get; set; }
        public string Type { get; set; }
        public int TypeId { get; set; }
        public decimal Value { get; set; }
        public string Comments { get; set; }
    }
}