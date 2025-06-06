using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FluentValidation;
using Sakila.Contracts.Services;
using Sakila.Contracts.Languages.Commands;
using Sakila.Contracts.Languages.Validators;
using Sakila.Contracts.Countries.Commands;
using Sakila.Contracts.Countries.Validators;
using Sakila.Web.Common;
using Sakila.Web.Services;

namespace Sakila.Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static WebAssemblyHostBuilder AddSakilaServices(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped<IApiClient, ApiClient>();
        builder.Services.AddScoped<ILanguageService, LanguageService>();
        builder.Services.AddScoped<ICountryService, CountryService>();
        builder.Services.AddTransient<IValidator<LanguageCreateRequest>, LanguageCreateValidator>();
        builder.Services.AddTransient<IValidator<LanguageUpdateRequest>, LanguageUpdateValidator>();
        builder.Services.AddTransient<IValidator<LanguageDeleteRequest>, LanguageDeleteValidator>();
        builder.Services.AddTransient<IValidator<CountryCreateRequest>, CountryCreateValidator>();
        builder.Services.AddTransient<IValidator<CountryUpdateRequest>, CountryUpdateValidator>();
        builder.Services.AddTransient<IValidator<CountryDeleteRequest>, CountryDeleteValidator>();
        builder.Services.AddScoped(sp => new HttpClient
            { BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]!) });
        return builder;
    }
}