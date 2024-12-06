using Issuel.Application.Common.Interfaces;
using Issuel.Application.Common.Services;
using Issuel.Infrastructure.Data;
using Issuel.Infrastructure.Repository;
using Issuel.Infrastructure.Repository.Repositories;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddNpgsql<IssueDbContext>(builder.Configuration.GetConnectionString("DbConnection"))
        .AddScoped<IUnitOfWork, UnitOfWork<IssueDbContext>>()
        .AddScoped<ILabelRepository, LabelRepository>()
        .AddScoped<IIssueRepository, IssueRepository>()
        .AddScoped<ILabelService, LabelService>();
    
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.MapControllers();

    app.Run();
}