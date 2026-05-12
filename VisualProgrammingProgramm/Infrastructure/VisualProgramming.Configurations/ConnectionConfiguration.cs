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
        builder.Property(x => x.Id).IsRequired();


        builder.HasOne(x => x.InElementGraf).WithMany("_elementGrafConnection");
    }
}
