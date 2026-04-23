using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using VisualProgramming.Domain.Entites;


namespace VisualProgramming.Configurations;

public class NodePortConnectionConfiguration : IEntityTypeConfiguration<NodePortConnection>
{
    public void Configure(EntityTypeBuilder<NodePortConnection> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.HasOne(x => x.Node).WithMany("nodePortConnections");
        builder.HasOne(x => x.Port).WithMany("nodePortConnections");
    }
}
