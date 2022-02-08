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
            try
            {
                return await _pokemonRepository.GetAsync(name);
            }
            catch (Exception e)
            {
                _logger.LogError("An exception occurred while trying to retrieve a Pokemon from the Pokemon repository.", e);
                throw;
            }
        }

        public async Task<Abstractions.Pokemon> GetTranslatedPokemonAsync(string name)
        {
            Abstractions.Pokemon pokemon;
            try
            {
                pokemon = await _pokemonRepository.GetAsync(name);
            }
            catch (Exception e)
            {
                _logger.LogError("An exception occurred while trying to retrieve a Pokemon from the Pokemon repository.", e);
                throw;
            }

            if (pokemon == null)
            {
                return null;
            }

            Translation translation = null;
            try
            {
                if (pokemon.Habitat == Abstractions.Habitat.Cave || pokemon.IsLegendary)
                {
                    translation = await _translationRepository.GetTranslationAsync(pokemon.Description, Language.Yoda);
                }
                else
                {
                    translation = await _translationRepository.GetTranslationAsync(pokemon.Description, Language.Shakespeare);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"An exception occurred while trying to translate a Pokemon's description.", e);
            }

            pokemon.Description = translation == null ? pokemon.Description : translation.Text;

            return pokemon;
        }
    }
}
