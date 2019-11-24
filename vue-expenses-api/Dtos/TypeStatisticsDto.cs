namespace vue_expenses_api.Dtos
{
    public class TypeStatisticsDto
    {
        protected TypeStatisticsDto()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Spent { get; set; }
    }
}