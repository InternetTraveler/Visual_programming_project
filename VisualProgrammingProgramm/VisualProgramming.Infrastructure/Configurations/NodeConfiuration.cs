using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VisualProgramming.Domain.Entites;


namespace VisualProgramming.Configurations;

public class NodeConfiuration : IEntityTypeConfiguration<Node>
{
    public void Configure(EntityTypeBuilder<Node> builder)
    {
        builder.Property(x => x.TypeOperation)
            .IsRequired();
    }
}
