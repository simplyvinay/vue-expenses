namespace vue_expenses_api.Dtos
{
    public class ExpenseTypeDto 
    {
        protected ExpenseTypeDto()
        {
        }

        public ExpenseTypeDto(
            int id,
            string name,
            string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
    }
}