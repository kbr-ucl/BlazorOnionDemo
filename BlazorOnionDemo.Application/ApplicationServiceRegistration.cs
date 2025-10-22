using BlazorOnionDemo.Application.CommandHandlers;
using BlazorOnionDemo.Application.Contracts.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorOnionDemo.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthorCommand, AuthorCommand>();
        services.AddScoped<IBookCommand, BookCommand>();

        return services;
    }
}