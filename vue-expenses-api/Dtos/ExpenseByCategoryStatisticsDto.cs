namespace vue_expenses_api.Dtos
{
    public class ExpenseByCategoryStatisticsDto
    {
        protected ExpenseByCategoryStatisticsDto()
        {
        }

        public ExpenseByCategoryStatisticsDto(
            string categoryName,
            decimal categoryColour,
            int month,
            decimal spent)
        {
            CategoryName = categoryName;
            CategoryColour = categoryColour;
            Month = month;
            Spent = spent;
        }

        public string CategoryName { get; set; }
        public decimal CategoryColour { get; set; }
        public int Month { get; set; }
        public decimal Spent { get; set; }
    }
}