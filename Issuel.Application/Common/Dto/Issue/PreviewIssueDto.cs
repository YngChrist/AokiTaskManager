using Issuel.Application.Common.Dto.Label;
using Issuel.Domain;

namespace Issuel.Application.Common.Dto.Issue;

public class PreviewIssueDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public LabelDto[] Labels { get; set; }
    public Priority Priority { get; set; }
}