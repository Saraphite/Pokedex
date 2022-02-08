using System.Threading.Tasks;

namespace Pokedex.Data.Translations
{
    public interface ITranslationRepository
    {
        /// <summary>
        /// Gets a translation of the text provided into the language requested.
        /// </summary>
        /// <param name="text">The text to be translated.</param>
        /// <param name="language">The language to translate the text to.</param>
        /// <returns>The translation data.</returns>
        public Task<Translation> GetTranslationAsync(string text, Language language);
    }
}
