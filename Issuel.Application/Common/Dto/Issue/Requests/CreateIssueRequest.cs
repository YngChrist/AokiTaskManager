using Issuel.Application.Common.Dto.Label.Requests;

namespace Issuel.Application.Common.Dto.Issue.Requests;

public class CreateIssueRequest : BaseIssueRequest
{
    public List<CreateLabelRequest> Labels { get; init; } = [];
}