namespace Shared.Domain.Exceptions;

/// <summary>
/// Ошибка, возникающая, когда сущность с заданным id не был найден.
/// </summary>
public class EntityNotFoundException : Exception
{
    /// <summary>
    /// Ошибка, возникающая, когда сущность с заданным id не был найден.
    /// </summary>
    public EntityNotFoundException(string? message) : base(message)
    {
    }
}