using Shared.Domain.Exceptions;
using Task.Domain.Entities;

namespace Task.Domain.Validation;

/// <summary>
/// Повторяющиеся инструменты валидации данных.
/// </summary>
public static class ThrowIf
{
    /// <summary>
    /// Проверка на то, что сущность не null.
    /// </summary>
    /// <param name="entity">Сущность для проверки.</param>
    /// <param name="id">Идентификатор, заданный для поиска.</param>
    /// <param name="paramName">Название параметра.</param>
    /// <exception cref="EntityNotFoundException">Ошибка, возникающая если entity - null.</exception>
    public static void EntityIsNull<T>(BaseEntity<T>? entity, Guid id, string paramName) where T : BaseEntity<T>
    {
        if (entity == null)
        {
            throw new EntityNotFoundException(ExceptionMessage.EntityNotFound(paramName, id));
        }
    }

    /// <summary>
    /// Проверка на то, что сущность не null. Без идентификатора.
    /// </summary>
    /// <param name="entity">Сущность для проверки.</param>
    /// /// <param name="paramName">Название параметра.</param>
    /// <exception cref="EntityNotFoundException">Ошибка, возникающая если entity - null.</exception>
    public static void EntityIsNull<T>(BaseEntity<T>? entity, string paramName) where T : BaseEntity<T>
    {
        if (entity == null)
        {
            throw new EntityNotFoundException(ExceptionMessage.EntityDoesntExists(paramName));
        }
    }
}