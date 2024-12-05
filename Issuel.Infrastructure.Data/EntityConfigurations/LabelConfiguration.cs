using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task.Domain.Entities;
using Task.Domain.Validation;

namespace Issuel.Infrastructure.Data.EntityConfigurations;

public class LabelConfiguration : IEntityTypeConfiguration<Label>
{
    public void Configure(EntityTypeBuilder<Label> builder)
    {
        builder.ToTable(nameof(Label));

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasColumnName(nameof(Label.Name))
            .IsRequired()
            .HasMaxLength(ValidationConstant.MaxLabelLength);
    }
}