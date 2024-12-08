using Issuel.Domain;

namespace Issuel.Application.Common.Dto.Issue.Requests;

public abstract class BaseIssueRequest
{
    public string Title { get; init; } = null!;
    public string? Description { get; init; }
    public Priority Priority { get; init; }
    public DateTime? Deadline { get; init; }
    public TimeSpan? Estimate { get; init; }
}