using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Sakila.API.Middleware;
using Sakila.API.Options;
using Sakila.Application.Common.Handlers;
using Sakila.Application.Common.Validation;
using Sakila.Application.Countries.Commands.Validators;
using Sakila.Application.Countries.Commands.Validators.Data;
using Sakila.Application.Countries.Queries.Validators;
using Sakila.Application.Countries.Queries.Validators.Data;
using Sakila.Application.Languages.Commands.Validators;
using Sakila.Application.Languages.Commands.Validators.Data;
using Sakila.Application.Languages.Queries.Mapping;
using Sakila.Application.Languages.Queries.Validators;
using Sakila.Application.Languages.Queries.Validators.Data;
using Sakila.Contracts.Countries.Commands;
using Sakila.Contracts.Countries.Queries;
using Sakila.Contracts.Languages.Commands;
using Sakila.Contracts.Languages.Queries;
using Sakila.Application.Common.Interfaces;
using Sakila.Infrastructure.Data;

namespace Sakila.API.Extensions;

public static class ServiceCollectionExtensions
{
    private const string AppPolicyName = "AllowSakilaWeb";
    private const string AppName = "Sakila.API";
    private const string AppVersion = "v1";

    public static void AddApplicationLayer(this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        var webOptions = configuration.GetSection("App.Web").Get<AppWebOptions>()!;
        services.AddCors(options =>
        {
            options.AddPolicy(AppPolicyName,
                policy => { policy.WithOrigins(webOptions.Endpoint).AllowAnyMethod().AllowAnyHeader(); });
        });


        services.AddDbContext<SakilaContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            if (environment.IsDevelopment())
                options.EnableSensitiveDataLogging()
                    .LogTo(Console.WriteLine, LogLevel.Information);
        });
        services.AddScoped<ISakilaContext>(sp => sp.GetRequiredService<SakilaContext>());

        services
            .AddMediatR(serviceConfiguration =>
                serviceConfiguration.RegisterServicesFromAssembly(typeof(CreateHandlerBase<,,>).Assembly))
            .AddAutoMapper(typeof(GetByIdProfile).Assembly);

        services.AddTransient<IValidator<LanguageCreateRequest>, LanguageCreateValidator>();
        services.AddTransient<IValidatorWithData<LanguageUpdateRequest, LanguageUpdateValidatorData>,
            LanguageUpdateValidator>();
        services.AddTransient<IValidatorWithData<LanguageDeleteRequest, LanguageDeleteValidatorData>,
            LanguageDeleteValidator>();
        services.AddTransient<IValidatorWithData<LanguageGetByIdRequest, LanguageGetByIdValidatorData>,
            LanguageGetByIdValidator>();

        services.AddTransient<IValidator<CountryCreateRequest>, CountryCreateValidator>();
        services.AddTransient<IValidatorWithData<CountryUpdateRequest, CountryUpdateValidatorData>,
            CountryUpdateValidator>();
        services.AddTransient<IValidatorWithData<CountryDeleteRequest, CountryDeleteValidatorData>,
            CountryDeleteValidator>();
        services.AddTransient<IValidatorWithData<CountryGetByIdRequest, CountryGetByIdValidatorData>,
            CountryGetByIdValidator>();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(AppVersion, new OpenApiInfo
            {
                Title = AppName,
                Version = AppVersion
            });
        });
    }

    public static void AddWebApplicationLayer(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/swagger/{AppVersion}/swagger.json", $"{AppName} {AppVersion}");
                options.RoutePrefix = "swagger";
            });
        }

        app.UseCors(AppPolicyName);
    }
}