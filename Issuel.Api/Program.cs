using Issuel.Api.Common.Extensions;
using Issuel.Api.Common.Middleware;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddInfrastructure(builder.Configuration)
        .AddApplication()
        .AddWeb();
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