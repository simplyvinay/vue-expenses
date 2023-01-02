namespace vue_expenses_api.Dtos;

public class CurrencyDto 
{
    public CurrencyDto()
    {

    }

    public CurrencyDto(
        string id,
        string name)
    {
        Id = id;
        Name = name;
    }

    public string Id { get; set; }
    public string Name { get; set; }

    public override bool Equals(object obj)
    {
        var item = obj as CurrencyDto;
        return item != null && Name.Equals(item.Name);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}