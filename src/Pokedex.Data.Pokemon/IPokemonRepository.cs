using System;
using System.Threading.Tasks;

namespace Pokedex.Data.Pokemon
{
    public interface IPokemonRepository
    {
        public Task<Abstractions.Pokemon> GetAsync(string name);
    }
}
