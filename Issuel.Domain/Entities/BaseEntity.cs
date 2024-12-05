namespace Issuel.Domain.Entities;

/// <summary>
/// Базовый класс entity.
/// </summary>
public abstract class BaseEntity<T> : IEquatable<T> where T : BaseEntity<T>
{
    /// <summary>
    /// Уникальный идентификатор.
    /// </summary>
    public Guid Id { get; protected set; }

    /// <summary>
    /// Базовый класс entity с заданием Id.
    /// </summary>
    protected BaseEntity(Guid id)
    {
        Id = id;
    }

    /// <summary>
    /// Базовый класс entity без задания Id.
    /// </summary>
    protected BaseEntity()
    {
    }

    /// <summary>
    /// Получение Hash Code.
    /// </summary>
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    /// <summary>
    /// Сравнение объекта с другим объектом.
    /// </summary>
    /// <param name="obj">Объект, с которым производится сравнение.</param>
    public override bool Equals(object? obj)
    {
        return obj is T other && Equals(other);
    }

    /// <summary>
    /// Сравнение объекта с другим объектом этого же типа.
    /// </summary>
    /// <param name="other">Объект, с которым производится сравнение.</param>
    public virtual bool Equals(T? other)
    {
        return Id == other?.Id;
    }
}