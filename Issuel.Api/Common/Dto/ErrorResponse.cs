using System.Collections;

namespace Issuel.Api.Common.Dto;

/// <summary>
/// Ответ с описанием возникшей ошибки.
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// Http код ошибки.
    /// </summary>
    public int StatusCode { get; init; }

    /// <summary>
    /// Тип ошибки.
    /// </summary>
    public string Type { get; init; } = null!;

    /// <summary>
    /// Трассировка - источник ошибки.
    /// </summary>
    public string? StackTrace { get; init; }

    /// <summary>
    /// Сообщение об ошибке.
    /// </summary>
    public string Message { get; init; } = null!;

    /// <summary>
    /// Дополнительные данные.
    /// </summary>
    public IDictionary Data { get; init; } = null!;
}