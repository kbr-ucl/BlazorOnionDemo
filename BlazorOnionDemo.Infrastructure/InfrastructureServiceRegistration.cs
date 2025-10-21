using BlazorOnionDemo.Application.Contracts.Queries;
using BlazorOnionDemo.Application.Repositories;
using BlazorOnionDemo.Infrastructure.Database;
using BlazorOnionDemo.Infrastructure.Queries;
using BlazorOnionDemo.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorOnionDemo.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register Query implementations
        services.AddScoped<IAuthorQuery, AuthorQuery>();
        services.AddScoped<IBookQuery, BookQuery>();

        // Register Repository implementations
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IBookRepository, BookRepository>();

        // Database
        // https://github.com/dotnet/SqlClient/issues/2239
        // https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/projects?tabs=dotnet-core-cli
        // Add-Migration InitialMigration -Context OrmContext -Project BlazorOnionDemo.Infrastructure
        // Update-Database -Context OrmContext -Project BlazorOnionDemo.Infrastructure
        services.AddDbContext<OrmContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DbConnection"));
        });

        return services;
    }
}