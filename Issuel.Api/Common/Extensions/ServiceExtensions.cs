using Ardalis.GuardClauses;
using Issuel.Api.Common.Json;
using Issuel.Api.Common.Middleware;
using Issuel.Application.Common.Interfaces;
using Issuel.Application.Common.Mappings;
using Issuel.Application.Common.Services;
using Issuel.Infrastructure.Data;
using Issuel.Infrastructure.Repository;
using Issuel.Infrastructure.Repository.Repositories;

namespace Issuel.Api.Common.Extensions;

/// <summary>
/// Расширение сервисов.
/// </summary>
public static class ServiceExtensions
{
    /// <summary>
    /// Добавить сервисы из инфраструктуры.
    /// </summary>
    /// <param name="services">Сервисы.</param>
    /// <param name="configuration">Конфигурация.</param>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConnectionString = Guard.Against.NullOrWhiteSpace(configuration.GetConnectionString("DefaultConnection"));

        services
            .AddNpgsql<IssueDbContext>(dbConnectionString)
            .AddScoped<IUnitOfWork, UnitOfWork<IssueDbContext>>()
            .AddScoped<IIssueRepository, IssueRepository>();
        
        return services;
    }

    /// <summary>
    /// Добавить сервисы из инфраструктуры.
    /// </summary>
    /// <param name="services">Сервисы.</param>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddTransient<LabelMapper>()
            .AddTransient<IssueMapper>()
            .AddScoped<IIssueService, IssueService>();
        
        return services;
    }

    /// <summary>
    /// Добавить сервисы из инфраструктуры.
    /// </summary>
    /// <param name="services">Сервисы.</param>
    public static IServiceCollection AddWeb(this IServiceCollection services)
    {
        services
            .AddSingleton<GlobalExceptionHandlerMiddleware>()
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new TimeSpanJsonConverter());
            });
    
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();

        return services;
    }
}