using System.Threading.Tasks;

namespace Pokedex.Data.Translations
{
    public interface ITranslationRepository
    {
        public Task<Translation> GetTranslationAsync(string text, Language language);
    }
}
