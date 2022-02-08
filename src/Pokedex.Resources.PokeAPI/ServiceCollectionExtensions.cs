using Microsoft.Extensions.DependencyInjection;
using PokeApiNet;
using Pokedex.Data.Pokemon;

namespace Pokedex.Resources.PokeAPI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPokeAPIResource(this IServiceCollection services)
        {
            /* Here we're using a third-party package (PokeApiNet) for communicating with the PokeAPI. 
             * In a production setting, I'd have written this myself so that I can; 
             * a) ensure that the design meets any standards I have.
             * b) ensure that I have ownership over any performance issues.
             * c) ensure that any new updates to the API (which may break the client used here) are quickly resolvable without reliance on a third-party.
             * d) configure the BaseAddress of the HttpClient. This package does not support this as far as I can ascertain.
             * e) doesn't block me from migrating to .NET 6.0
             */
            services.AddHttpClient<PokeApiClient>();
            services.AddSingleton<IPokemonRepository, PokemonRepository>();

            return services;
        }
    }
}
