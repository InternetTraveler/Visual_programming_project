using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using VisualProgramming.Domain.Entites;
using VisualProgramming.ValueObject;


namespace VisualProgramming.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Name)
            .IsRequired(false)
            .HasConversion(name => name.Value, str => new Name(str))
            .HasMaxLength(50);

        builder.HasMany<Graf>("_grafs")
            .WithOne(x => x.Project)
            .HasForeignKey("id_project")
            .HasPrincipalKey(x => x.Id);

        builder.Ignore(x => x.Grafs);
    }
}