using Microsoft.EntityFrameworkCore;
using VisualProgramming.Domain.Entites;

namespace VisualProgramming.Migrations;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Project> Projects { get; set; }
    public DbSet<Graf> Grafs { get; set; }
    public DbSet<ElementGraf> ElementGrafs { get; set; }
    public DbSet<Node> Nodes { get; set; }
    public DbSet<Modul> Moduls { get; set; }
    public DbSet<Connection> Connections { get; set; }
    public DbSet<NodePortConnection> NodePortConnections { get; set; }
    public DbSet<Port> Ports { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }


}