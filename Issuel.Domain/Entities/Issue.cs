using Ardalis.GuardClauses;
using FluentValidation;
using Issuel.Domain.Extensions;
using Issuel.Domain.Validation;
using Issuel.Domain.Validation.Validators;

namespace Issuel.Domain.Entities;

/// <summary>
/// Задача.
/// </summary>
public class Issue : BaseEntity<Issue>
{
    private readonly List<Label> _labels;
 
    /// <summary>
    /// Заголовок.
    /// </summary>
    public string Title { get; private set; }
    
    /// <summary>
    /// Описание.
    /// </summary>
    public string? Description { get; private set; }
    
    /// <summary>
    /// Приоритет.
    /// </summary>
    public Priority Priority { get; private set; }
    
    /// <summary>
    /// Статус.
    /// </summary>
    public Status Status { get; private set; }
    
    /// <summary>
    /// Срок выполнения.
    /// </summary>
    public DateTime? Deadline { get; private set; }
    
    /// <summary>
    /// Оценка времени выоплнения.
    /// </summary>
    public TimeSpan? Estimate { get; private set; }
    
    /// <summary>
    /// Потрачено.
    /// </summary>
    public TimeSpan? Spent { get; private set; }
    
    /// <summary>
    /// Список меток.
    /// </summary>
    public IReadOnlyCollection<Label> Labels => _labels.AsReadOnly();

    /// <summary>
    /// Задача.
    /// </summary>
    public Issue(string title, Priority priority, string? description = null, DateTime? deadline = null, TimeSpan? estimate = null)
    {
        Title = title;
        Status = Status.ToDo;
        Priority = priority;
        _labels = [];

        Description = description;
        Deadline = deadline;
        Estimate = estimate;
        
        new IssueValidator().ValidateAndThrow(this);
    }

    public void Update(string title, Priority priority, Status status, string? description = null, DateTime? deadline = null, TimeSpan? estimate = null)
    {
        Title = title;
        Priority = priority;
        Status = status;
        Description = description;
        Deadline = deadline;
        Estimate = estimate;

        new IssueValidator().ValidateAndThrow(this);
    }

    /// <summary>
    /// Задача. Оставлено для EF.
    /// </summary>
    protected Issue()
    {
    }

    /// <summary>
    /// Добавить / Обновить метку.
    /// </summary>
    /// <param name="label">Метка.</param>
    public void SaveLabel(Label label)
    {
        var innerLabel = _labels.FirstOrDefault(l => l.Name == label.Name);

        if (innerLabel == null)
        {
            _labels.Add(label);
        }
        else
        {
            innerLabel.Update(label.Name);
        }
    }
    
    /// <summary>
    /// Удаление метки по id.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    public void DeleteLabel(Guid id)
    {
        Guard.Against.DefaultValue(id, nameof(id));
        
        var label = _labels.FirstOrDefault(x => x.Id == id);
        ThrowIf.EntityIsNull(label, id, nameof(Label));
        
        _labels.Remove(label!);
    }
}