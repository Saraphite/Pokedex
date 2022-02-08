using Microsoft.Extensions.DependencyInjection;

namespace Pokedex.Managers.Pokemon
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the Pokemon Manager to the Service Collection.
        /// </summary>
        public static IServiceCollection AddPokemonManager(this IServiceCollection services)
        {
            services.AddSingleton<PokemonManager>();
            return services;
        }
    }
}
