using System;

namespace Pokedex.Data.Pokemon.Exceptions
{
    public class PokemonUnavailableException : Exception
    {
        public PokemonUnavailableException(Exception e) : base("Unable to resolve requested Pokemon. Please see inner exception for more details.", e)
        {

        }
    }
}
