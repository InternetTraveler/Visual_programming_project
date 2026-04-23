using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using VisualProgramming.Domain.Entites;

namespace VisualProgramming.Configurations;

public class GrafConfiguration : IEntityTypeConfiguration<Graf>
{
    public void Configure(EntityTypeBuilder<Graf> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.HasOne(x => x.Project).WithMany("_grafs");

        builder.HasMany<ElementGraf>("elementsGraf")
            .WithOne(x => x.ParentGraf)
            .HasForeignKey("id_element_graf")
            .HasPrincipalKey(x => x.Id);
        builder.Ignore(x => x.ElementsGraf);
    }
}
