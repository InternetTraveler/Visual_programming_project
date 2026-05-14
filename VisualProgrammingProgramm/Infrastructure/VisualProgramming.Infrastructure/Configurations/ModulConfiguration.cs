using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VisualProgramming.Domain.Entites;


namespace VisualProgramming.Configurations;

public class ModulConfiguration : IEntityTypeConfiguration<Modul>
{
    public void Configure(EntityTypeBuilder<Modul> builder)
    {
        builder.HasOne(x => x.GrafOperation).WithMany().HasForeignKey("id_operation");
    }
}
