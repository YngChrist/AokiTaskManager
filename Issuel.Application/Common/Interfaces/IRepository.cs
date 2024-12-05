using Issuel.Domain.Entities;

namespace Issuel.Application.Common.Interfaces;

/// <summary>
/// Базовый контракт работы хранилища.
/// </summary>
/// /// <typeparam name="T">Сущность.</typeparam>
public interface IRepository<T> where T : BaseEntity<T>
{
    /// <summary>
    /// Получение сущности с заданным идентификатором.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Возвращает сущность <c>c отслеживанием</c>.</returns>
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавление сущности в репозиторий.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаление сущности из репозитория.
    /// </summary>
    /// <param name="entity">Сущность.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
}