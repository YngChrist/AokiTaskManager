using Ardalis.GuardClauses;
using Issuel.Application.Common.Dto.Issue;
using Issuel.Application.Common.Dto.Issue.Requests;
using Issuel.Application.Common.Interfaces;
using Issuel.Application.Common.Mappings;
using Issuel.Domain;
using Issuel.Domain.Entities;
using Issuel.Domain.Validation;

namespace Issuel.Application.Common.Services;

/// <summary>
/// Сервис, работающий с Issue/
/// </summary>
public class IssueService : IIssueService
{
    private readonly IIssueRepository _issueRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IssueMapper _issueMapper;
    private readonly LabelMapper _labelMapper;

    /// <summary>
    /// Сервис, работающий с Issue/
    /// </summary>
    public IssueService(IIssueRepository issueRepository, IssueMapper issueMapper, IUnitOfWork unitOfWork, LabelMapper labelMapper)
    {
        _issueRepository = issueRepository;
        _issueMapper = issueMapper;
        _unitOfWork = unitOfWork;
        _labelMapper = labelMapper;
    }

    /// <summary>
    /// Получение задачи по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Задача.</returns>
    public async Task<IssueDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Guard.Against.Default(id, nameof(id));
        
        var issue = await _issueRepository.GetByIdAsync(id, cancellationToken);
        ThrowIf.EntityIsNull(issue, id, nameof(Issue));
        
        return _issueMapper.Map(issue!);
    }

    /// <summary>
    /// Получение всех невыполненных задач.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Массив предварительных данных задач.</returns>
    public async Task<PreviewIssueDto[]> GetAllUndoneAsync(CancellationToken cancellationToken = default)
    {
        var issues = await _issueRepository
            .SearchAsync<PreviewIssueDto>(x => x.Status != Status.Done, 
                x => new PreviewIssueDto
                {
                    Id = x.Id, 
                    Title = x.Title, 
                    Labels = _labelMapper.Map(x.Labels),
                    Priority = x.Priority,
                },cancellationToken);

        return issues;
    }

    /// <summary>
    /// Получение всех выполненных задач.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Массив предварительных данных задач.</returns>
    public async Task<PreviewIssueDto[]> GetAllDoneAsync(CancellationToken cancellationToken = default)
    {
        var issues = await _issueRepository
            .SearchAsync<PreviewIssueDto>(x => x.Status == Status.Done, 
                x => new PreviewIssueDto
                {
                    Id = x.Id, 
                    Title = x.Title, 
                    Labels = _labelMapper.Map(x.Labels),
                    Priority = x.Priority
                },cancellationToken);

        return issues;
    }

    /// <summary>
    /// Получение всех задач.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Массив предварительных данных всех задач.</returns>
    public async Task<PreviewIssueDto[]> GetAllAsync(CancellationToken cancellationToken)
    {
        var issues = await _issueRepository
            .GetAllAsync<PreviewIssueDto>(x => new PreviewIssueDto
                {
                    Id = x.Id, 
                    Title = x.Title, 
                    Labels = _labelMapper.Map(x.Labels),
                    Priority = x.Priority
                },cancellationToken);

        return issues;
    }

    /// <summary>
    /// Создание новой задачи.
    /// </summary>
    /// <param name="request">Запрос на создание задачи.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Созданная задача.</returns>
    public async Task<IssueDto> CreateAsync(CreateIssueRequest request, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(request, nameof(CreateIssueRequest));
        
        var issue = new Issue(request.Title, request.Priority, request.Description, request.Deadline, request.Estimate);
        request.Labels.ForEach(x => issue.SaveLabel(new Label(x.Name)));
        
        await _issueRepository.AddAsync(issue, cancellationToken);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return _issueMapper.Map(issue);
    }

    /// <summary>
    /// Обновление задачи.
    /// </summary>
    /// <param name="request">Запрос на обновление задачи.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Обновлённая задача.</returns>
    public async Task<IssueDto> UpdateAsync(UpdateIssueRequest request, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(request, nameof(UpdateIssueRequest));
        
        var issue = await _issueRepository.GetByIdAsync(request.Id, cancellationToken);
        ThrowIf.EntityIsNull(issue, request.Id, nameof(Issue));
        
        issue!.Update(request.Title, request.Priority, request.Status, request.Description, request.Deadline, request.Estimate);
        
        var labelsForDelete = issue.Labels
            .Where(x => request.Labels.All(y => y.Id != x.Id))
            .Select(x => x.Id)
            .ToArray();

        foreach (var label in labelsForDelete)
        {
            issue.DeleteLabel(label);
        }

        foreach (var label in request.Labels)
        {
            issue.SaveLabel(new Label(label.Name));
        }
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _issueMapper.Map(issue);
    }

    /// <summary>
    /// Удаление задачи по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор задачи.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    /// <returns>Задача завершения операции.</returns>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Guard.Against.Default(id, nameof(id));
        
        var issue = await _issueRepository.GetByIdAsync(id, cancellationToken);
        ThrowIf.EntityIsNull(issue, id, nameof(Issue));
        
        await _issueRepository.DeleteAsync(issue!, cancellationToken);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}