using Ardalis.GuardClauses;
using Issuel.Application.Common.Interfaces;
using Issuel.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Issuel.Infrastructure.Repository;

/// <summary>
/// Базовая реализация репозитория.
/// </summary>
/// <typeparam name="T">Сущность.</typeparam>
/// <typeparam name="TContext">Контекст базы данных.</typeparam>
public abstract class BaseRepository<T, TContext> : IRepository<T> where T : BaseEntity<T> where TContext : DbContext
{
    /// <summary>
    /// Контекст базы данных.
    /// </summary>
    protected readonly TContext Context;

    /// <summary>
    /// Базовая реализация репозитория.
    /// </summary>
    protected BaseRepository(TContext context)
    {
        Context = context;
    }

    /// <summary>
    /// Получение сущности с заданным идентификатором.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Возвращает сущность <c>c отслеживанием</c>.</returns>
    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Guard.Against.Default(id);

        var entity = await Context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity;
    }

    /// <summary>
    /// Добавление сущности в репозиторий.
    /// </summary>
    /// <param name="entity">Сущность на добавление.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    public virtual async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await Context.Set<T>().AddAsync(entity, cancellationToken);
    }

    /// <summary>
    /// Удаление сущности из репозитория.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    public virtual Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        Context.Set<T>().Remove(entity);

        return Task.CompletedTask;
    }
}