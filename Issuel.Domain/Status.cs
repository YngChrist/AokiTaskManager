namespace Issuel.Domain;

/// <summary>
/// Статус выполнения задачи.
/// </summary>
public enum Status
{
    /// <summary>
    /// Неопределен.
    /// </summary>
    Undefined = 0,
    
    /// <summary>
    /// В списке на выполнение.
    /// </summary>
    ToDo = 1,
    
    /// <summary>
    /// В процессе выполнения.
    /// </summary>
    InProgress = 2,
    
    /// <summary>
    /// Завершена.
    /// </summary>
    Done = 3
}