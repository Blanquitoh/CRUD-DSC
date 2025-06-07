using FluentValidation;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Sakila.Contracts.Countries.Commands;
using Sakila.Contracts.Countries.Commands.Validators;
using Sakila.Contracts.Languages.Commands;
using Sakila.Contracts.Languages.Commands.Validators;
using Sakila.Contracts.Services;
using Sakila.Web.Common;
using Sakila.Web.Services;

namespace Sakila.Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static WebAssemblyHostBuilder AddSakilaServices(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped<IApiClient, ApiClient>();

        builder.Services.AddScoped<ILanguageService, LanguageService>();
        builder.Services.AddTransient<IValidator<LanguageCreateRequest>, LanguageCreateValidator>();
        builder.Services.AddTransient<IValidator<LanguageUpdateRequest>, LanguageUpdateValidator>();

        builder.Services.AddScoped<ICountryService, CountryService>();
        builder.Services.AddTransient<IValidator<CountryCreateRequest>, CountryCreateValidator>();
        builder.Services.AddTransient<IValidator<CountryUpdateRequest>, CountryUpdateValidator>();

        builder.Services.AddScoped(sp => new HttpClient
            { BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]!) });
        return builder;
    }
}
