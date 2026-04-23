using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using VisualProgramming.Migrations;

namespace VisualProgramming.Infrastructure.Migrations;

[DbContext(typeof(ApplicationDbContext))]
partial class ApplicationDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.8")
            .HasAnnotation("Relational:MaxIdentifierLength", 63);

        NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

        modelBuilder.Entity("VisualProgramming.Domain.Entites.Project", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid");

            b.Property<string>("Name")
                .IsRequired()
                .HasColumnType("text");

            b.HasKey("Id");

            b.ToTable("Projects");
        });

        modelBuilder.Entity("VisualProgramming.Domain.Entites.Graf", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid");

            b.Property<Guid>("ProjectId")
                .HasColumnType("uuid");

            b.HasKey("Id");

            b.HasIndex("ProjectId");

            b.ToTable("Grafs");
        });

        modelBuilder.Entity("VisualProgramming.Domain.Entites.ElementGraf", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid");

            b.Property<Guid?>("NodeId")
                .HasColumnType("uuid");

            b.Property<int>("LevelLevelOfDepthOperation")
                .HasColumnType("integer");

            b.Property<bool>("IsModul")
                .HasColumnType("boolean");

            b.Property<Guid>("ParentGrafId")
                .HasColumnType("uuid");

            b.Property<double>("PositionX")
                .HasColumnType("double precision");

            b.Property<double>("PositionY")
                .HasColumnType("double precision");

            b.HasKey("Id");

            b.HasIndex("NodeId");

            b.HasIndex("ParentGrafId");

            b.ToTable("ElementGrafs");
        });

        modelBuilder.Entity("VisualProgramming.Domain.Entites.BaseNode", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid");

            b.Property<string>("Name")
                .IsRequired()
                .HasColumnType("text");

            b.Property<string>("Discriminator")
                .IsRequired()
                .HasMaxLength(21)
                .HasColumnType("character varying(21)");

            b.HasKey("Id");

            b.ToTable("Nodes");

            b.HasDiscriminator<string>("Discriminator").HasValue("BaseNode");

            b.UseTphMappingStrategy();
        });

        modelBuilder.Entity("VisualProgramming.Domain.Entites.Port", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid");

            b.Property<Guid>("NodeId")
                .HasColumnType("uuid");

            b.Property<int>("TypePort")
                .HasColumnType("integer");

            b.Property<string>("Description")
                .IsRequired()
                .HasColumnType("text");

            b.HasKey("Id");

            b.HasIndex("NodeId");

            b.ToTable("Ports");
        });

        modelBuilder.Entity("VisualProgramming.Domain.Entites.Connection", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid");

            b.Property<Guid>("SourcePortId")
                .HasColumnType("uuid");

            b.Property<Guid>("TargetPortId")
                .HasColumnType("uuid");

            b.Property<Guid>("InElementGrafId")
                .HasColumnType("uuid");

            b.Property<Guid>("OutElementGrafId")
                .HasColumnType("uuid");

            b.HasKey("Id");

            b.HasIndex("SourcePortId");

            b.HasIndex("TargetPortId");

            b.HasIndex("InElementGrafId");

            b.HasIndex("OutElementGrafId");

            b.ToTable("Connections");
        });

        modelBuilder.Entity("VisualProgramming.Domain.Entites.NodePortConnection", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid");

            b.Property<Guid>("NodeId")
                .HasColumnType("uuid");

            b.Property<Guid>("PortId")
                .HasColumnType("uuid");

            b.HasKey("Id");

            b.HasIndex("NodeId");

            b.HasIndex("PortId");

            b.ToTable("NodePortConnections");
        });

        modelBuilder.Entity("VisualProgramming.Domain.Entites.Node", b =>
        {
            b.HasBaseType("VisualProgramming.Domain.Entites.BaseNode");

            b.Property<int>("TypeOperation")
                .HasColumnType("integer");

            b.HasDiscriminator().HasValue("Node");
        });

        modelBuilder.Entity("VisualProgramming.Domain.Entites.Modul", b =>
        {
            b.HasBaseType("VisualProgramming.Domain.Entites.BaseNode");

            b.Property<Guid>("GrafOperationId")
                .HasColumnType("uuid");

            b.HasIndex("GrafOperationId");

            b.HasDiscriminator().HasValue("Modul");
        });

        modelBuilder.Entity("VisualProgramming.Domain.Entites.Graf", b =>
        {
            b.HasOne("VisualProgramming.Domain.Entites.Project", "Project")
                .WithMany("Grafs")
                .HasForeignKey("ProjectId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Project");
        });

        modelBuilder.Entity("VisualProgramming.Domain.Entites.ElementGraf", b =>
        {
            b.HasOne("VisualProgramming.Domain.Entites.BaseNode", "Node")
                .WithMany()
                .HasForeignKey("NodeId");

            b.HasOne("VisualProgramming.Domain.Entites.Graf", "ParentGraf")
                .WithMany("ElementsGraf")
                .HasForeignKey("ParentGrafId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Node");

            b.Navigation("ParentGraf");
        });

        modelBuilder.Entity("VisualProgramming.Domain.Entites.Port", b =>
        {
            b.HasOne("VisualProgramming.Domain.Entites.BaseNode", "Node")
                .WithMany()
                .HasForeignKey("NodeId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Node");
        });

        modelBuilder.Entity("VisualProgramming.Domain.Entites.Connection", b =>
        {
            b.HasOne("VisualProgramming.Domain.Entites.Port", "SourcePort")
                .WithMany()
                .HasForeignKey("SourcePortId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("VisualProgramming.Domain.Entites.Port", "TargetPort")
                .WithMany()
                .HasForeignKey("TargetPortId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("VisualProgramming.Domain.Entites.ElementGraf", "InElementGraf")
                .WithMany()
                .HasForeignKey("InElementGrafId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("VisualProgramming.Domain.Entites.ElementGraf", "OutElementGraf")
                .WithMany()
                .HasForeignKey("OutElementGrafId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("InElementGraf");

            b.Navigation("OutElementGraf");

            b.Navigation("SourcePort");

            b.Navigation("TargetPort");
        });

        modelBuilder.Entity("VisualProgramming.Domain.Entites.NodePortConnection", b =>
        {
            b.HasOne("VisualProgramming.Domain.Entites.BaseNode", "Node")
                .WithMany("NodePortConnections")
                .HasForeignKey("NodeId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("VisualProgramming.Domain.Entites.Port", "Port")
                .WithMany("NodePortConnections")
                .HasForeignKey("PortId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Node");

            b.Navigation("Port");
        });

        modelBuilder.Entity("VisualProgramming.Domain.Entites.Modul", b =>
        {
            b.HasOne("VisualProgramming.Domain.Entites.Graf", "GrafOperation")
                .WithMany()
                .HasForeignKey("GrafOperationId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("GrafOperation");
        });

        modelBuilder.Entity("VisualProgramming.Domain.Entites.Project", b =>
        {
            b.Navigation("Grafs");
        });

        modelBuilder.Entity("VisualProgramming.Domain.Entites.Graf", b =>
        {
            b.Navigation("ElementsGraf");
        });

        modelBuilder.Entity("VisualProgramming.Domain.Entites.BaseNode", b =>
        {
            b.Navigation("NodePortConnections");
        });

        modelBuilder.Entity("VisualProgramming.Domain.Entites.Port", b =>
        {
            b.Navigation("NodePortConnections");
        });
    }
}