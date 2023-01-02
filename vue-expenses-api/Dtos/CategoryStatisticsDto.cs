namespace vue_expenses_api.Dtos;

public class CategoryStatisticsDto
{
    protected CategoryStatisticsDto()
    {
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Budget { get; set; }
    public decimal Spent { get; set; }
    public string Colour { get; set; }
    public int Month { get; set; }
}