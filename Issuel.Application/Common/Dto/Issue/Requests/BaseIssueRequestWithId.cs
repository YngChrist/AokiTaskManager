namespace Issuel.Application.Common.Dto.Issue.Requests;

public abstract class BaseIssueRequestWithId
{
    public Guid Id { get; init; }
}