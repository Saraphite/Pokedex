using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        public async Task<IActionResult> GetAsync([FromRoute] string name)
        {
            _logger.LogTrace($"GET request received for Pokemon: {name}.");
            var pokemon = await _pokemonManager.GetPokemonAsync(name);

            if (pokemon == null) return NotFound(null);
            return Ok(pokemon);
        }

        [HttpGet]
        [Route("[controller]/translated/{name}")]
        public async Task<IActionResult> GetWithTranslation([FromRoute] string name)
        {
            _logger.LogTrace($"GET request received for translated Pokemon: {name}.");
            var pokemon = await _pokemonManager.GetTranslatedPokemonAsync(name);

            if (pokemon == null) return NotFound(null);
            return Ok(pokemon);
        }
    }
}
