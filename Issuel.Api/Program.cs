using Issuel.Api.Common.Json;
using Issuel.Api.Common.Middleware;
using Issuel.Application.Common.Interfaces;
using Issuel.Application.Common.Mappings;
using Issuel.Application.Common.Services;
using Issuel.Infrastructure.Data;
using Issuel.Infrastructure.Repository;
using Issuel.Infrastructure.Repository.Repositories;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddSingleton<GlobalExceptionHandlerMiddleware>()
        .AddNpgsql<IssueDbContext>(builder.Configuration.GetConnectionString("DbConnection"))
        .AddScoped<IUnitOfWork, UnitOfWork<IssueDbContext>>()
        .AddTransient<LabelMapper>()
        .AddTransient<IssueMapper>()
        .AddScoped<ILabelRepository, LabelRepository>()
        .AddScoped<IIssueRepository, IssueRepository>()
        .AddScoped<ILabelService, LabelService>()
        .AddScoped<IIssueService, IssueService>()
        .AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new TimeSpanJsonConverter());
        });
    
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
{
    app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.MapControllers();
    
    app.MapGet("api/ping", () => "Pong");

    app.Run();
}