using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task.Domain.Entities;
using Task.Domain.Validation;

namespace Issuel.Infrastructure.Data.EntityConfigurations;

public class IssueConfiguration : IEntityTypeConfiguration<Issue>
{
    public void Configure(EntityTypeBuilder<Issue> builder)
    {
        builder.ToTable(nameof(Issue));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Title)
            .HasColumnName(nameof(Issue.Title))
            .IsRequired()
            .HasMaxLength(ValidationConstant.MaxLabelLength);

        builder.Property(x => x.Status)
            .HasColumnName(nameof(Issue.Status))
            .IsRequired();
        
        builder.Property(x => x.Priority)
            .HasColumnName(nameof(Issue.Priority))
            .IsRequired();
        
        builder.Property(x => x.Description)
            .HasColumnName(nameof(Issue.Description))
            .IsRequired(false);
        
        builder.Property(x => x.Deadline)
            .HasColumnName(nameof(Issue.Deadline))
            .IsRequired(false);

        builder.Property(x => x.Estimate)
            .HasColumnName(nameof(Issue.Estimate))
            .IsRequired(false);
        
        builder.Property(x => x.Spent)
            .HasColumnName(nameof(Issue.Spent))
            .IsRequired(false);

        builder.HasMany(x => x.Labels)
            .WithOne()
            .OnDelete(DeleteBehavior.NoAction);
    }
}