using Microsoft.Extensions.Logging;
using Pokedex.Data.Pokemon;
using Pokedex.Data.Translations;
using System;
using System.Threading.Tasks;

namespace Pokedex.Managers.Pokemon
{
    public class PokemonManager
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly ITranslationRepository _translationRepository;
        private readonly ILogger<PokemonManager> _logger;

        public PokemonManager(IPokemonRepository pokemonRepository, ITranslationRepository translationRepository, ILogger<PokemonManager> logger)
        {
            _pokemonRepository = pokemonRepository;
            _translationRepository = translationRepository;
            _logger = logger;
        }

        public async Task<Abstractions.Pokemon> GetPokemonAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Abstractions.Pokemon> GetTranslatedPokemonAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
