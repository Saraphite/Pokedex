using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pokedex.Abstractions;
using System.Threading.Tasks;

namespace Pokedex.Controllers
{
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly ILogger<PokemonController> _logger;

        public PokemonController(ILogger<PokemonController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("[controller]/{name}")]
        public async Task<Pokemon> GetAsync([FromRoute] string name)
        {
            var pokemon = new Pokemon
            {
                Name = name
            };

            return await Task.FromResult(pokemon);
        }

        [HttpGet]
        [Route("[controller]/translated/{name}")]
        public async Task<Pokemon> GetWithTranslationAsync([FromRoute] string name)
        {
            var pokemon = new Pokemon
            {
                Name = name
            };

            return await Task.FromResult(pokemon);
        }
    }
}
