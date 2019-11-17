namespace vue_expenses_api.Dtos
{
    public class CategoryStatisticsDto
    {
        protected CategoryStatisticsDto()
        {
        }

        public CategoryStatisticsDto(
            int id,
            string name,
            decimal budget,
            decimal spent,
            string colour)
        {
            Id = id;
            Name = name;
            Budget = budget;
            Spent = spent;
            Colour = colour;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public decimal Spent { get; set; }
        public string Colour { get; set; }
    }
}