using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VisualProgramming.Domain.Entites;
using VisualProgramming.ValueObject;


namespace VisualProgramming.Configurations;

public class ElementGrafConfiguration : IEntityTypeConfiguration<ElementGraf>
{
    public void Configure(EntityTypeBuilder<ElementGraf> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.HasOne(x => x.Node).WithMany()
            .HasForeignKey("id_node");

        builder.Property(x => x.PositionX).IsRequired();
        builder.Property(x => x.PositionY).IsRequired();

        builder.Property(x => x.IsModul).IsRequired();

        builder.HasOne(x => x.ParentGraf).WithMany("elementsGraf");

        builder.HasMany(e => e.ElementGrafConnections)
            .WithOne()
            .HasForeignKey("id_element_graf");

        builder.Property(e => e.LevelLevelOfDepthOperation)
            .HasConversion(inp => inp.Value, outp => new LevelOfDepth(outp))
            .IsRequired();
    }
}
