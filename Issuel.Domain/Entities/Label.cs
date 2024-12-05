namespace Issuel.Domain.Entities;

public class Label : BaseEntity<Label>
{
    public string Name { get; private set; }

    public Label(string name)
    {
        Name = name;
    }
    
    protected Label()
    {
    }

    public void Update(string name)
    {
        Name = name;
    }
}