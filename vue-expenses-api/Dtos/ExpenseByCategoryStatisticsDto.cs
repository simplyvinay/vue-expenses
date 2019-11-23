namespace vue_expenses_api.Dtos
{
    public class ExpenseByCategoryStatisticsDto
    {
        protected ExpenseByCategoryStatisticsDto()
        {
        }

        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryColour { get; set; }
        public int Month { get; set; }
        public decimal Spent { get; set; }
    }

    public class ExpenesesByCategorySeriesDto
    {
        public ExpenesesByCategorySeriesDto()
        {
            
        }

        public string Name { get; set; }
        public string Type { get; set; }
        public string Stack { get; set; }
        public string BarWidth { get; set; }
        public object Label { get; set; }
        public object ItemStyle { get; set; }
        public decimal[] Data { get; set; }
    }
}