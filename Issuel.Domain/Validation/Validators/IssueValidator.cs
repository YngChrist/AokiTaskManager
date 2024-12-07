using FluentValidation;
using Issuel.Domain.Entities;

namespace Issuel.Domain.Validation.Validators;

public class IssueValidator : AbstractValidator<Issue>
{
    public IssueValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(ValidationConstant.MaxTitleLength);

        RuleFor(x => x.Deadline)
            .GreaterThanOrEqualTo(DateTime.UtcNow);

        RuleFor(x => x.Status)
            .IsInEnum()
            .NotEqual(Status.Undefined);
        
        RuleFor(x => x.Priority)
            .IsInEnum()
            .NotEqual(Priority.Undefined);
    }
}