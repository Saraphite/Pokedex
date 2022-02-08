using Microsoft.Extensions.DependencyInjection;

namespace Pokedex.Managers.Pokemon
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPokemonManager(this IServiceCollection services)
        {
            services.AddSingleton<PokemonManager>();
            return services;
        }
    }
}
