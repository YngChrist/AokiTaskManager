using Issuel.Application.Common.Interfaces;
using Issuel.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Issuel.Infrastructure.Repository;

/// <summary>
/// Базовая реализация UnitOfWork.
/// </summary>
/// <typeparam name="TContext">Контекст базы данных.</typeparam>
public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
{
    /// <summary>
    /// Контекст базы данных.
    /// </summary>
    private readonly TContext _context;

    /// <summary>
    /// Базовая реализация UnitOfWork.
    /// </summary>
    protected UnitOfWork(TContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Сохранение внесенных изменений.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException {SqlState: PostgresErrorCodes.UniqueViolation})
        {
            var entity = ex.Entries[0].Entity;
            throw new EntityAlreadyExistsException(ExceptionMessage.EntityAlreadyExists(entity.GetType().Name));
        }
    }
}