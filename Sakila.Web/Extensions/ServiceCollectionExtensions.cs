using FluentValidation;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Refit;
using Sakila.Contracts.Countries.Commands;
using Sakila.Contracts.Countries.Commands.Validators;
using Sakila.Contracts.Languages.Commands;
using Sakila.Contracts.Languages.Commands.Validators;
using Sakila.Web.Abstractions;
using Sakila.Web.Api;
using Sakila.Web.Services.Implementations;

namespace Sakila.Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddSakilaServices(this WebAssemblyHostBuilder builder)
    {
        var apiBaseUrl = new Uri(builder.Configuration["ApiBaseUrl"]!);

        builder.Services.AddRefitClient<ILanguagesApi>().ConfigureHttpClient(client => client.BaseAddress = apiBaseUrl);
        builder.Services.AddScoped<ILanguageService, LanguageService>();
        builder.Services.AddTransient<IValidator<LanguageCreateRequest>, LanguageCreateValidator>();
        builder.Services.AddTransient<IValidator<LanguageUpdateRequest>, LanguageUpdateValidator>();

        builder.Services.AddRefitClient<ICountriesApi>().ConfigureHttpClient(client => client.BaseAddress = apiBaseUrl);
        builder.Services.AddScoped<ICountryService, CountryService>();
        builder.Services.AddTransient<IValidator<CountryCreateRequest>, CountryCreateValidator>();
        builder.Services.AddTransient<IValidator<CountryUpdateRequest>, CountryUpdateValidator>();

        builder.Services.AddMudServices();
    }
}