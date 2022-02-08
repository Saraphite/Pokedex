using System.Threading.Tasks;

namespace Pokedex.Data.Pokemon
{
    public interface IPokemonRepository
    {
        /// <summary>
        /// Gets a Pokémon from the repository.
        /// </summary>
        /// <param name="name">The name of the Pokémon.</param>
        /// <returns>The Pokémon.</returns>
        /// <exception cref="PokemonUnavailableException">An exception that occurs if there's any dependency exceptions that cannot be resolved.</exception>
        public Task<Abstractions.Pokemon> GetAsync(string name);
    }
}
