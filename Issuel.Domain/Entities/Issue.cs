using Issuel.Domain.Validation;

namespace Issuel.Domain.Entities;

/// <summary>
/// Задача.
/// </summary>
public class Issue : BaseEntity<Issue>
{
    private readonly List<Label> _labels;
    
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public Priority Priority { get; private set; }
    public Status Status { get; private set; }
    public DateTime? Deadline { get; private set; }
    public TimeSpan? Estimate { get; private set; }
    public TimeSpan? Spent { get; private set; }
    public IReadOnlyCollection<Label> Labels => _labels.AsReadOnly();


    public Issue(string title, Priority priority, string? description = null, DateTime? deadline = null, TimeSpan? estimate = null)
    {
        Title = title;
        Status = Status.ToDo;
        Priority = priority;
        _labels = new List<Label>();

        Description = description;
        Deadline = deadline;
        Estimate = estimate;
    }

    public void Update(string title, Priority priority, Status status, string? description = null, DateTime? deadline = null, TimeSpan? estimate = null)
    {
        Title = title;
        Priority = priority;
        Status = status;
        Description = description;
        Deadline = deadline;
        Estimate = estimate;
    }

    protected Issue()
    {
    }

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
    
    public void DeleteLabel(Guid id)
    {
        var label = _labels.FirstOrDefault(x => x.Id == id);
        ThrowIf.EntityIsNull(label, id, nameof(Label));
        
        _labels.Remove(label!);
    }
}