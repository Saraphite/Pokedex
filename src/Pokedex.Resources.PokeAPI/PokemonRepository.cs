using PokeApiNet;
using Pokedex.Data.Pokemon;
using System.Linq;
using System.Threading.Tasks;
using PokeApiPokemon = PokeApiNet.Pokemon;
using Pokemon = Pokedex.Abstractions.Pokemon;

namespace Pokedex.Resources.PokeAPI
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly PokeApiClient _client;

        public PokemonRepository(PokeApiClient client)
        {
            _client = client;
        }

        public async Task<Pokemon> GetAsync(string name)
        {
            var pokemon = await _client.GetResourceAsync<PokeApiPokemon>(name);
            var species = await _client.GetResourceAsync(pokemon.Species);
            var habitat = await _client.GetResourceAsync(species.Habitat);

            var result = new Pokemon()
            {
                Name = pokemon.Name,
                Description = species.FlavorTextEntries.FirstOrDefault().FlavorText,
                Habitat = habitat.ToHabitat(),
                IsLegendary = species.IsLegendary
            };

            return result;
        }
    }
}
