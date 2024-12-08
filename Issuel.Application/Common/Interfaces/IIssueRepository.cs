using System.Linq.Expressions;
using Issuel.Domain.Entities;

namespace Issuel.Application.Common.Interfaces;

public interface IIssueRepository : IRepository<Issue>
{
    Task<TPreview[]> GetAllAsync<TPreview>(Expression<Func<Issue, TPreview>> select, CancellationToken cancellationToken = default);
    
    
    /// <summary>
    /// Получение всех задач по фильтру.
    /// </summary>
    /// <param name="filter">Фильтр.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    Task<Issue[]> SearchAsync(Expression<Func<Issue, bool>> filter, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получение всех задач по фильтру.
    /// </summary>
    /// <param name="filter">Фильтр.</param>
    /// <param name="select">Делегат выборки данных.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    Task<TPreview[]> SearchAsync<TPreview>(Expression<Func<Issue, bool>> filter, Expression<Func<Issue, TPreview>> select, CancellationToken cancellationToken = default);
}