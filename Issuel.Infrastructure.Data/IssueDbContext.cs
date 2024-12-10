using Issuel.Domain;
using Issuel.Domain.Entities;
using Issuel.Infrastructure.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Issuel.Infrastructure.Data;

public class IssueDbContext : DbContext
{
    public DbSet<Issue> Issues => Set<Issue>();

    public IssueDbContext(DbContextOptions options) : base(options)
    {
    }

    public IssueDbContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<Status>();
        modelBuilder.HasPostgresEnum<Priority>();
        modelBuilder.ApplyConfiguration(new LabelConfiguration());
        modelBuilder.ApplyConfiguration(new IssueConfiguration());
    }
}