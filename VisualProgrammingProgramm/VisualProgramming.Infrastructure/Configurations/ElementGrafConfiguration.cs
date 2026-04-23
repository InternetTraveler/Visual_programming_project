using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using VisualProgramming.Domain.Entites;


namespace VisualProgramming.Configurations;

public class ElementGrafConfiguration : IEntityTypeConfiguration<ElementGraf>
{
    public void Configure(EntityTypeBuilder<ElementGraf> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.HasOne(x => x.Node); //?????????????

        builder.Property(x => x.PositionX).IsRequired();
        builder.Property(x => x.PositionY).IsRequired();

        builder.Property(x => x.IsModul).IsRequired();

        builder.HasOne(x => x.ParentGraf).WithMany("elementsGraf");
    }
}
