namespace vue_expenses_api.Dtos
{
    public class ExpenseCategoryDto
    {
        protected ExpenseCategoryDto()
        {
        }

        public ExpenseCategoryDto(
            int id,
            string name,
            string description,
            decimal budget,
            string colourHex)
        {
            Id = id;
            Name = name;
            Description = description;
            Budget = budget;
            ColourHex = colourHex;
        }

        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public decimal Budget { get; }
        public string ColourHex { get; }
    }
}