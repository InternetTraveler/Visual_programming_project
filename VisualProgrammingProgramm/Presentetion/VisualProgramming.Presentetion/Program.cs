using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using VisualProgramming.Infrastructure;
using VisualProgramming.Presentetion.Healper;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString(nameof(ApplicationDbContext));

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string for NotesServiceDbContext is not configured.");
        }

        builder.Services.AddNpgsql<ApplicationDbContext>(connectionString, options =>
        {
            options.MigrationsAssembly("VisualProgramming.Infrastructure");

        });

        builder.Services.AddSwaggerGen(
            c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Notes service API",
                    Description = "API for creating, viewing, storing, modifying, and deleting notes."
                });
            });

        builder.Services.AddDbContext<ApplicationDbContext>(
            options =>
            {
                options.UseNpgsql(connectionString);
            });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            //app.UseSwaggerUI();
        }

        app.UseAuthorization();


        app.MapControllers();

        app.MigrateDatabase<ApplicationDbContext>();

        app.Run();
    }
}