using Issuel.Domain.Entities;

namespace Issuel.Application.Common.Interfaces;

public interface ILabelRepository : IRepository<Label>
{
    Task<Label[]> GetAllAsync(CancellationToken cancellationToken = default);
}