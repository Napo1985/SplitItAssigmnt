using Microsoft.OpenApi.Models;
using Splitit.App.Middlewares;
using Splitit.Infra.Providers;
using Splitit.Infra.Repositories;
using Splitit.Splitit.Repositories;
using Splitit.Splitit.Services;
using Swashbuckle.AspNetCore.Filters;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        builder.Configuration.AddEnvironmentVariables();

        ConfigureServices(builder);

        var app = builder.Build();
        Configure(app);

        app.Run();
    }

    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        var services = builder.Services;

        services.AddControllers();
        services.AddHttpClient();
        services.AddSingleton<IActorRepository, InMemoryActorRepository>();

        var configuration = builder.Configuration;
        var actorProviderConfig = configuration.GetSection("ActorProviders");

        //for providers we should use dynamic factory (at this scope will not be implemnted due to time limitation) 
        services.AddTransient<IActorProvider, ImdbActorProvider>();
        services.AddScoped<ActorService>();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Splitit API", Version = "Home Assignment" });
            c.ExampleFilters();
        });
        services.AddSwaggerExamplesFromAssemblyOf<Program>();
    }

    private static void Configure(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseAuthorization();
        app.MapControllers();
    }
}