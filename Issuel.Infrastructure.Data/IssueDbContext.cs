﻿using Issuel.Infrastructure.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Task.Domain;
using Task.Domain.Entities;

namespace Issuel.Infrastructure.Data;

public class IssueDbContext : DbContext
{
    public DbSet<Issue> Issues => Set<Issue>();
    public DbSet<Label> Labels => Set<Label>();

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