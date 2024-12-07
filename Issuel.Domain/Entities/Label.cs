using FluentValidation;
using Issuel.Domain.Validation.Validators;

namespace Issuel.Domain.Entities;

public class Label : BaseEntity<Label>
{
    public string Name { get; private set; }

    public Label(string name)
    {
        Name = name;

        new LabelValidator().ValidateAndThrow(this);
    }
    
    protected Label()
    {
    }

    public void Update(string name)
    {
        Name = name;

        new LabelValidator().ValidateAndThrow(this);
    }
}