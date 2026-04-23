using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using VisualProgramming.Domain.Entites;


namespace VisualProgramming.Configurations;

public class NodeConfiuration : IEntityTypeConfiguration<Node>
{
    public void Configure(EntityTypeBuilder<Node> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.TypeOperation).IsRequired();

        builder.HasMany<NodePortConnection>("nodePortConnections")
            .WithOne(x => (Node)x!.Node!)
            .HasForeignKey("id_node")
            .HasPrincipalKey(x => x.Id);

        builder.Ignore(x => x.NodePortConnections);
    }
}
