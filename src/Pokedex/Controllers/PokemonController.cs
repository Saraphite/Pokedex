using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pokedex.Abstractions;
using Pokedex.Managers.Pokemon;
using System.Threading.Tasks;

namespace Pokedex.Controllers
{
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly PokemonManager _pokemonManager;
        private readonly ILogger<PokemonController> _logger;

        public PokemonController(PokemonManager pokemonManager, ILogger<PokemonController> logger)
        {
            _pokemonManager = pokemonManager;
            _logger = logger;
        }

        [HttpGet]
        [Route("[controller]/{name}")]
        public async Task<Pokemon> GetAsync([FromRoute] string name)
        {
            _logger.LogTrace($"GET request received for Pokemon: {name}.");
            return await _pokemonManager.GetPokemonAsync(name);
        }

        [HttpGet]
        [Route("[controller]/translated/{name}")]
        public async Task<Pokemon> GetWithTranslationAsync([FromRoute] string name)
        {
            _logger.LogTrace($"GET request received for translated Pokemon: {name}.");
            return await _pokemonManager.GetTranslatedPokemonAsync(name);
        }
    }
}
