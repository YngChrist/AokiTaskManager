using Issuel.Application.Common.Dto.Label;
using Issuel.Domain;

namespace Issuel.Application.Common.Dto.Issue;

public class IssueDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public Priority Priority { get; set; }
    public Status Status { get; set; }
    public DateTime? Deadline { get; set; }
    public TimeSpan? Estimate { get; set; }
    public TimeSpan? Spent { get; set; }
    public List<LabelDto> Labels { get; set; } = [];
}