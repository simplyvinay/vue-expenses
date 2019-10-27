namespace vue_expense_api.Dtos
{
    public class ExpenseDto
    {
        public ExpenseDto(
            int id,
            int categoryId,
            int typeId,
            decimal value,
            string comments)
        {
            Id = id;
            CategoryId = categoryId;
            TypeId = typeId;
            Value = value;
            Comments = comments;
        }

        public int Id { get; }
        public string Category { get; set; }
        public int CategoryId { get; }
        public string Type { get; set; }
        public int TypeId { get; }
        public decimal Value { get; }
        public string Comments { get; }
    }
}