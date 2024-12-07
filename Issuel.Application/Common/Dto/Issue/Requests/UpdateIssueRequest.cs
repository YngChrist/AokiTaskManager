using Issuel.Domain;

namespace Issuel.Application.Common.Dto.Issue.Requests;

public class UpdateIssueRequest : BaseIssueRequest
{
    public Guid Id { get; init; }
    public Status Status { get; init; }
}