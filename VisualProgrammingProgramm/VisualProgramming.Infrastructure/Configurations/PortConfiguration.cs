using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using VisualProgramming.Domain.Entites;
using VisualProgramming.Domain.Enum;
using VisualProgramming.ValueObject;


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
            .HasMaxLength(50);

        builder.Property(x => x.TypePort)
            .IsRequired(); // как конвертировать

        builder.HasMany<NodePortConnection>("nodePortConnections")
            .WithOne(x => x.Port)
            .HasForeignKey("id_port")
            .HasPrincipalKey(x => x.Id);

        builder.Ignore(x => x.NodePortConnections);
    }
}
