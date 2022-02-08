using System;

namespace Pokedex.Data.Translations.Exceptions
{
    public class TranslationUnavailableException : Exception
    {
        public TranslationUnavailableException(Exception e) : base("Unable to retrieve translation for requested text. Please see inner exception for more details.", e)
        {

        }
    }
}
