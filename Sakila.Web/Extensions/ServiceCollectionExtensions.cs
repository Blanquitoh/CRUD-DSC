using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Sakila.Contracts.Services;
using Sakila.Web.Services;

namespace Sakila.Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static WebAssemblyHostBuilder AddSakilaServices(this WebAssemblyHostBuilder builder)
    {
        builder.Services.AddScoped<ILanguageService, LanguageService>();
        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]!) });
        return builder;
    }
}