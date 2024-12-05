namespace Issuel.Domain.Exceptions;

/// <summary>
/// Набор стандартных текстов ошибок.
/// </summary>
public static class ExceptionMessage
{
    /// <summary>
    /// Сообщение о неверном формате.
    /// </summary>
    public static string InvalidFormat(string paramName, Type type)
    {
        return $"Invalid format for {paramName}, type: {type}";
    }

    /// <summary>
    /// Сообщение о стандартном - неверном значении.
    /// </summary>
    public static string DefaultValue(string paramName, Type type)
    {
        return $"For {paramName} passed default value, that's invalid, type: {type}";
    }

    /// <summary>
    /// Сообщение о том, что был передан null, где это недопустимо.
    /// </summary>
    public static string ArgumentIsNull(string paramName)
    {
        return $"Parameter {paramName} must be not null";
    }

    /// <summary>
    /// Сообщение о том, что строка - null, где это не допустимо.
    /// </summary>
    public static string StringIsNullOrEmpty(string paramName)
    {
        return $"String {paramName} must be niether null nor empty";
    }

    /// <summary>
    /// Сообщение о том, что сущность с заданным id не найдена.
    /// </summary>
    public static string EntityNotFound(string entityName, Guid id)
    {
        return $"Entity {entityName} with id {id} not found";
    }

    /// <summary>
    /// Сообщение о том, что сущность не существует.
    /// </summary>
    public static string EntityDoesntExists(string entityName)
    {
        return $"Entity {entityName} does not exists";
    }

    /// <summary>
    /// Сообщение о том, что строка длиннее допустимого значения.
    /// </summary>
    public static string StringIsTooLong(string paramName, int length)
    {
        return $"String {paramName} should be not longer than {length}";
    }

    /// <summary>
    /// Сообщение о том, что сущность с заданным id не найдена.
    /// </summary>
    public static string EntityAlreadyExists(string entityName)
    {
        return $"Entity {entityName} already exists";
    }
}