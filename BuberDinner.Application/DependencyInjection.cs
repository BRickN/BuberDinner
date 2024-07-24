using System.Reflection;
using BuberDinner.Application.Common.Behaviors;
using BuberDinner.Application.Common.Mapping;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace BuberDinner.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));


        services.AddAutoMapper(typeof(ApplicationMappingProfile));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}