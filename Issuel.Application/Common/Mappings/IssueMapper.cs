using Issuel.Application.Common.Dto.Issue;
using Issuel.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Issuel.Application.Common.Mappings;

[Mapper]
public partial class IssueMapper
{
    public partial IssueDto Map(Issue entity);
    public partial IssueDto[] Map(Issue[] entity);
}