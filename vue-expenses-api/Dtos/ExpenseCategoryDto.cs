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

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Budget { get; set; }
        public string ColourHex { get; set; }
    }
}