namespace Issuel.Application.Common.Dto.Label.Requests;

public class UpdateLabelRequest : BaseLabelRequestWithId
{
    public string Name { get; set; } = null!;
}