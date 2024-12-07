using FluentValidation;
using Issuel.Domain.Entities;

namespace Issuel.Domain.Validation.Validators;

public class LabelValidator : AbstractValidator<Label>
{
    public LabelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(ValidationConstant.MaxLabelLength);
    }
}