using System.Linq.Expressions;
using Ardalis.GuardClauses;
using Issuel.Application.Common.Interfaces;
using Issuel.Domain.Entities;
using Issuel.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Issuel.Infrastructure.Repository.Repositories;

public class IssueRepository : BaseRepository<Issue, IssueDbContext>, IIssueRepository
{
    public IssueRepository(IssueDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Получение сущности с заданным идентификатором.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Возвращает сущность <c>c отслеживанием</c>.</returns>
    public override Task<Issue?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Context.Issues.Include(x => x.Labels)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<TPreview[]> GetAllAsync<TPreview>(Expression<Func<Issue, TPreview>> select, CancellationToken cancellationToken = default)
    {
        return Context.Issues
            .AsNoTracking()
            .Include(x => x.Labels)
            .OrderBy(x => x.Status)
            .ThenByDescending(x => x.Priority)
            .Select(select)
            .ToArrayAsync(cancellationToken);
    }

    /// <summary>
    /// Получение всех задач по фильтру.
    /// </summary>
    /// <param name="filter">Фильтр.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    public Task<Issue[]> SearchAsync(Expression<Func<Issue, bool>> filter, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(filter);
        
        return Context.Issues
            .AsNoTracking()
            .Include(x => x.Labels)
            .Where(filter)
            .ToArrayAsync(cancellationToken);
    }

    /// <summary>
    /// Получение всех задач по фильтру.
    /// </summary>
    /// <param name="filter">Фильтр.</param>
    /// <param name="select">Делегат выборки данных.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    public Task<TPreview[]> SearchAsync<TPreview>(Expression<Func<Issue, bool>> filter, Expression<Func<Issue, TPreview>> select, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(filter);
        Guard.Against.Null(select);
        
        return Context.Issues
            .AsNoTracking()
            .Include(x => x.Labels)
            .Where(filter)
            .Select(select)
            .ToArrayAsync(cancellationToken);
    }
    
}