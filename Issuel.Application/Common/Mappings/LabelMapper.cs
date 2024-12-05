using Issuel.Application.Common.Dto.Label;
using Issuel.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Issuel.Application.Common.Mappings;

[Mapper]
public partial class LabelMapper
{
    public partial LabelDto Map(Label label);
    
    public partial LabelDto[] Map(Label[] labels);
}