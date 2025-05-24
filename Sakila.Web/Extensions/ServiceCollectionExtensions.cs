using Sakila.Contracts.Services;
using Sakila.Web.Services;

namespace Sakila.Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSakilaServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddHttpClient<ILanguageService, LanguageService>(client =>
        {
            client.BaseAddress = new Uri(configuration["ApiBaseUrl"]!);
        });
        return services;
    }
}
