using Microsoft.Extensions.Logging;
using PokeApiNet;
using Pokedex.Data.Pokemon;
using Pokedex.Data.Pokemon.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using PokeApiPokemon = PokeApiNet.Pokemon;
using Pokemon = Pokedex.Abstractions.Pokemon;

namespace Pokedex.Resources.PokeAPI
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly PokeApiClient _client;
        private readonly ILogger<PokemonRepository> _logger;

        public PokemonRepository(PokeApiClient client, ILogger<PokemonRepository> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<Pokemon> GetAsync(string name)
        {
            try
            {
                var pokemon = await _client.GetResourceAsync<PokeApiPokemon>(name);
                var species = await _client.GetResourceAsync(pokemon.Species);
                var habitat = await _client.GetResourceAsync(species.Habitat);

                var result = new Pokemon()
                {
                    Id = pokemon.Id,
                    Name = pokemon.Name,
                    Description = species.FlavorTextEntries.FirstOrDefault().FlavorText,
                    Habitat = habitat.ToHabitat(),
                    IsLegendary = species.IsLegendary
                };

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError("An exception occurred while trying to retrieve data from PokeAPI.", e);
                throw new PokemonUnavailableException(e);
            }
        }
    }
}
