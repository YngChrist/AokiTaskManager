namespace Issuel.Application.Common.Interfaces;

/// <summary>
/// Контракт сохранения внесенных изменений в репозиториях.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Сохранение внесенных изменений.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}