using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using VisualProgramming.Domain.Entites;


namespace VisualProgramming.Configurations;

public class ConnectionConfiguration : IEntityTypeConfiguration<Connection>
{
    public void Configure(EntityTypeBuilder<Connection> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(c => c.SourcePort)
            .WithMany()
            .HasForeignKey("source_port_id")
            .IsRequired();

        builder.HasOne(c => c.TargetPort)
            .WithMany()
            .HasForeignKey("target_port_id")
            .IsRequired();

        builder.HasOne(c => c.InElementGraf)
            .WithMany()
            .HasForeignKey("in_element_graf_id")
            .IsRequired();

        builder.HasOne(c => c.OutElementGraf)
            .WithMany()
            .HasForeignKey("out_element_graf_id")
            .IsRequired();
    }
}
