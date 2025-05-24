using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Sakila.Application.Common.Behaviors;
using Sakila.Application.Languages.Commands.Handlers;
using Sakila.Application.Languages.Commands.Validators;
using Sakila.Application.Languages.Queries.Mapping;
using Sakila.Infrastructure.Data;

namespace Sakila.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSakilaApplication(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<SakilaContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information);
        });

        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateHandler).Assembly))
            .AddAutoMapper(typeof(GetByIdProfile).Assembly)
            .AddValidatorsFromAssembly(typeof(CreateValidator).Assembly)
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }

    public static IServiceCollection AddSakilaSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Sakila.API",
                Version = "v1"
            });
        });

        return services;
    }
}