using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisualProgramming.Domain.Base;
using VisualProgramming.Domain.Entites;
using VisualProgramming.ValueObject;

namespace VisualProgramming.Configurations;

public class BaseNodeConfiguration : IEntityTypeConfiguration<BaseNode>
{
    public void Configure(EntityTypeBuilder<BaseNode> builder)
    {
        builder.ToTable("BaseNodes");
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Name)
            .HasConversion(n => n.Value, v => new Name(v))
            .IsRequired();

        builder.HasMany<NodePortConnection>("nodePortConnections")
            .WithOne(x => (Modul)x.Node!)
            .HasForeignKey("id_node")
            .HasPrincipalKey(x => x.Id);

        builder.Ignore(x => x.NodePortConnections);

        builder.HasDiscriminator<string>("NodeType")
            .HasValue<Node>("Node")
            .HasValue<Modul>("Modul");
    }
}