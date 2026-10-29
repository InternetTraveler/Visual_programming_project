using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi;
using Npgsql;
using VisualProgramming.Infrastructure;
using VisualProgramming.Infrastructure.RepositoriesEF;
using VisualProgramming.Presentetion.Healper;
using VisualProgramming.Presentetion.Mapper;
using VisualProgramming.Repository;
using VisualProgramming.Repository.Base;
using VisualProgramming.Servises;
using VisualProgramming.Servises.Abstraction;
using VisualProgramming.Servises.Mapping;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // ---------- Configuration ----------
        var connectionString = builder.Configuration.GetConnectionString(nameof(ApplicationDbContext));
        if (string.IsNullOrEmpty(connectionString))
            throw new InvalidOperationException("Connection string for VisualProgrammingDbContext is not configured.");

        // ---------- EF Core / PostgreSQL ----------
        builder.Services.AddNpgsql<ApplicationDbContext>(connectionString, options =>
        {
            options.MigrationsAssembly("VisualProgramming.Infrastructure");
        });

        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

        // ---------- Swagger ----------
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Visual programming API",
                Description = "API for managing projects, graphs, elements, connections, modules, nodes and ports."
            });
        });

        builder.Services.AddAutoMapper(cfg => { }, typeof(ApplicationProfile), typeof(PresentationProfile));

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddScoped(typeof(IRepository<,>), typeof(EfRepository<,>));
        builder.Services.AddScoped<IProjectRepository, EfProjectRepository>();
        builder.Services.AddScoped<IGrafRepository, EfGrafRepository>();
        builder.Services.AddScoped<IElementGrafRepository, EfElementGrafRepository>();
        builder.Services.AddScoped<IConnectionRepository, EfConnectionRepository>();
        builder.Services.AddScoped<IModulRepository, EfModulRepository>();
        builder.Services.AddScoped<INodeRepository, EfNodeRepository>();
        builder.Services.AddScoped<INodePortConnectionRepository, EfNodePortConnectionRepository>();
        builder.Services.AddScoped<IPortRepository, EfPortRepository>();

        // ---------- Application services ----------
        builder.Services.AddScoped<IGraphReorganizer, GraphReorganizer>();
        builder.Services.AddScoped<ICascadeDeleteService, CascadeDeleteService>();
        builder.Services.AddScoped<IProjectAplicationService, ProjectApplicationService>();
        builder.Services.AddScoped<IGrafAplicationService, GrafApplicationService>();
        builder.Services.AddScoped<IElementGrafAplicationService, ElementGrafApplicationService>();
        builder.Services.AddScoped<IConnectionAplicationService, ConnectionApplicationService>();
        builder.Services.AddScoped<IModulAplicationService, ModulApplicationService>();
        builder.Services.AddScoped<INodeAplicationService, NodeApplicationService>();
        builder.Services.AddScoped<INodePortConnectionAplicationService, NodePortConnectionApplicationService>();
        builder.Services.AddScoped<IPortAplicationService, PortApplicationService>();

        builder.Services.AddHealthChecks()
        .AddCheck("npgsql", ct =>
        {
            using var conn = new NpgsqlConnection(connectionString);
            conn.OpenAsync(ct);
            return HealthCheckResult.Healthy();
        })
        .AddDbContextCheck<ApplicationDbContext>();

        // ---------- Build ----------
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();
        app.MapControllers();

        app.MigrateDatabase<ApplicationDbContext>();

        var mapper = app.Services.GetRequiredService<IMapper>();
        var config = mapper.ConfigurationProvider;

        foreach (var error in config.GetType().GetMethod("GetAllTypeMaps") is not null
            ? Array.Empty<string>()
            : Array.Empty<string>())
        {
        }

        // Проще — отловить AggregateException и вывести InnerExceptions:
        try
        {
            config.AssertConfigurationIsValid();
        }
        catch (AggregateException ex)
        {
            foreach (var inner in ex.InnerExceptions)
            {
                Console.WriteLine(inner.Message);
                Console.WriteLine("-----");
            }
        }

        app.Run();
    }
}