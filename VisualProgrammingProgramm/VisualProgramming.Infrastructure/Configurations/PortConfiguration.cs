using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Globalization;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Domain.Enum;
using VisualProgramming.ValueObject;
using VisualProgramming.ValueObject.Validais;


namespace VisualProgramming.Configurations;

public class PortConfiguration : IEntityTypeConfiguration<Port>
{
    public void Configure(EntityTypeBuilder<Port> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.Description)
            .IsRequired()
            .HasConversion(des => des.Value, str => new Description(str))
            .HasMaxLength(DescriptionValidator.MaxLenghts);

        builder.Property(x => x.TypePort)
            .IsRequired();

        builder.HasOne(p => p.Node)
            .WithMany()
            .HasForeignKey("NodeId")
            .IsRequired();

        builder.HasMany<NodePortConnection>("nodePortConnections")
            .WithOne(x => x.Port)
            .HasForeignKey("id_port")
            .HasPrincipalKey(x => x.Id);

        builder.Ignore(x => x.NodePortConnections);
    }
}
