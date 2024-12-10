using Ardalis.GuardClauses;
using FluentValidation;
using Issuel.Domain.Exceptions;

namespace Issuel.Domain.Extensions;

/// <summary>
/// Набор расширений дял валидации данных.
/// </summary>
public static class GuardClausesExtensions
{
    /// <summary>
    /// Проверка того, что вводимая строка не null и не пустая.
    /// </summary>
    /// <param name="input">Проверяемый параметр.</param>
    /// <param name="parameterName">Имя проверяемого параметра.</param>
    /// <exception cref="ValidationException">Ошибка возникающая в случае невалидного значения строки.</exception>
    public static string NullOrWhiteSpaceWithValidationException(this IGuardClause guardClause, string? input, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ValidationException(ExceptionMessage.StringIsNullOrEmpty(parameterName));
        }

        return input;
    }

    /// <summary>
    /// Проверка того, что вводимая строка не превышает лимита.
    /// </summary>
    /// <param name="input">Проверяемый параметр.</param>
    /// <param name="maxLength">Максимальная длина.</param>
    /// <param name="parameterName">Имя проверяемого параметра.</param>
    /// <exception cref="ValidationException">Ошибка возникающая в случае невалидного значения строки.</exception>
    public static void StringTooLongWithValidationException(this IGuardClause guardClause, string input, int maxLength, string parameterName)
    {
        if (input.Length > maxLength)
        {
            throw new ValidationException(ExceptionMessage.StringIsTooLong(parameterName, maxLength));
        }
    }

    /// <summary>
    /// Проверка того, что ввод не null.
    /// </summary>
    /// <param name="input">Проверяемый параметр.</param>
    /// <param name="parameterName">Имя проверяемого параметра.</param>
    /// <exception cref="ValidationException">Ошибка возникающая в случае если ввод - null.</exception>
    public static void NullWithValidationException<T>(this IGuardClause guardClause, T? input, string parameterName)
    {
        if (input == null)
        {
            throw new ValidationException(ExceptionMessage.ArgumentIsNull(parameterName));
        }
    }

    /// <summary>
    /// Проверка, чтобы значение не было равным значению по умолчанию для его типа.
    /// </summary>
    /// <param name="input">Проверяемое значение.</param>
    /// <param name="paramName">Имя проверяемого параметра.</param>
    /// <exception cref="ArgumentException">Ошибка возникающая в случае невалидного значения.</exception>
    public static void DefaultValue<T>(this IGuardClause guardClause, T input, string paramName) where T : struct
    {
        if (EqualityComparer<T>.Default.Equals(input, default))
        {
            throw new ArgumentException(ExceptionMessage.DefaultValue(paramName, typeof(T)));
        }
    }
}