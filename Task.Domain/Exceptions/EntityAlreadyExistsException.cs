namespace Shared.Domain.Exceptions;

/// <summary>
/// Ошибка, возникающая, когда сущность с заданным id уже существует.
/// </summary>
public class EntityAlreadyExistsException : Exception
{
    /// <summary>
    /// Ошибка, возникающая, когда сущность с заданным id уже существует.
    /// </summary>
    public EntityAlreadyExistsException(string? message) : base(message)
    {
    }
}