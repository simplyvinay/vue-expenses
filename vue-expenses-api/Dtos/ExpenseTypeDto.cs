namespace vue_expenses_api.Dtos;

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

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}