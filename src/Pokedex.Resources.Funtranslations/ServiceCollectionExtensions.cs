using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pokedex.Data.Translations;

namespace Pokedex.Resources.Funtranslations
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the Funtranslations Resource to the Service Collection.
        /// </summary>
        public static IServiceCollection AddFuntranslationsResource(this IServiceCollection services, IConfiguration config)
        {
            services.AddOptions<FuntranslationsOptions>().Bind(config.GetSection(FuntranslationsOptions.ConfigurationKey));

            services.AddHttpClient<FuntranslationsClient>();

            services.AddSingleton<ITranslationRepository, TranslationRepository>();

            return services;
        }
    }
}
