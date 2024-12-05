using Issuel.Domain.Entities;

namespace Issuel.Application.Common.Interfaces;

public interface IIssueRepository : IRepository<Issue>
{
    /// <summary>
    /// Получение всех задач по фильтру.
    /// </summary>
    /// <param name="filter">Фильтр.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    Task<Issue[]> SearchAsync(Func<Issue, bool> filter, CancellationToken cancellationToken = default);
}