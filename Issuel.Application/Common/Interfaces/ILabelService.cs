using Issuel.Application.Common.Dto.Label;
using Issuel.Application.Common.Dto.Label.Requests;

namespace Issuel.Application.Common.Interfaces;

public interface ILabelService
{
    Task<LabelDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<LabelDto[]> GetAllAsync(CancellationToken cancellationToken = default);
    Task<LabelDto> CreateAsync(CreateLabelRequest request, CancellationToken cancellationToken = default);
    Task<LabelDto> UpdateAsync(UpdateLabelRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}