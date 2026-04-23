using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using VisualProgramming.Domain.Entites;


namespace VisualProgramming.Configurations;

public class ModulConfiguration : IEntityTypeConfiguration<Modul>
{
    public void Configure(EntityTypeBuilder<Modul> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.HasOne(x => x.GrafOperation); //??????????

        builder.HasMany<NodePortConnection>("nodePortConnections")
            .WithOne(x => (Modul)x.Node!)
            .HasForeignKey("id_node")
            .HasPrincipalKey(x => x.Id);

        builder.Ignore(x => x.NodePortConnections);
    }
}
