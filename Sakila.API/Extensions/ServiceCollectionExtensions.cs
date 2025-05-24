using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Sakila.API.Options;
using Sakila.Application.Common.Behaviors;
using Sakila.Application.Languages.Commands.Handlers;
using Sakila.Application.Languages.Commands.Validators;
using Sakila.Application.Languages.Queries.Mapping;
using Sakila.Infrastructure.Data;

namespace Sakila.API.Extensions;

public static class ServiceCollectionExtensions
{
    private const string SakilaPolicyName = "AllowSakilaWeb";
    private const string AppName = "Sakila.API";
    private const string AppVersion = "v1";

    public static IServiceCollection AddSakilaApplication(this IServiceCollection services,
        IConfiguration configuration)
    {
        var webOptions = configuration.GetSection("Sakila.Web").Get<SakilaWebOptions>()!;
        services.AddCors(options =>
        {
            options.AddPolicy(SakilaPolicyName, policy =>
            {
                policy.WithOrigins(webOptions.Endpoint)
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });


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

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(AppVersion, new OpenApiInfo
            {
                Title = AppName,
                Version = AppVersion
            });
        });

        return services;
    }

    public static IApplicationBuilder AddSakilaApp(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", $"{AppName} {AppVersion}");
                options.RoutePrefix = "swagger";
            });
        }

        app.UseCors(SakilaPolicyName);

        return app;
    }
}