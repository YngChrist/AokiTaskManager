using Issuel.Application.Common.Dto.Issue;
using Issuel.Application.Common.Dto.Issue.Requests;

namespace Issuel.Application.Common.Interfaces;

/// <summary>
/// Контракт сервиса, работающего с Issue.
/// </summary>
public interface IIssueService
{
    /// <summary>
    /// Получение задачи по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Задача.</returns>
    Task<IssueDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Получение всех невыполненных задач.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Массив предварительных данных задач.</returns>
    Task<PreviewIssueDto[]> GetAllUndoneAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получение всех выполненных задач.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Массив предварительных данных задач.</returns>
    Task<PreviewIssueDto[]> GetAllDoneAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Создание новой задачи.
    /// </summary>
    /// <param name="request">Запрос на создание задачи.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Созданная задача.</returns>
    Task<IssueDto> CreateAsync(CreateIssueRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновление задачи.
    /// </summary>
    /// <param name="request">Запрос на обновление задачи.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Обновлённая задача.</returns>
    Task<IssueDto> UpdateAsync(UpdateIssueRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаление задачи по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор задачи.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Задача завершения операции.</returns>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получение всех задач.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Массив предварительных данных всех задач.</returns>
    Task<PreviewIssueDto[]> GetAllAsync(CancellationToken cancellationToken = default);
}