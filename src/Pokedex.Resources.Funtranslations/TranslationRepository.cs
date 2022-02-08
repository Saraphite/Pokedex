using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pokedex.Data.Translations;
using Pokedex.Data.Translations.Exceptions;
using Pokedex.Resources.Funtranslations.Models;
using System;
using System.Threading.Tasks;

namespace Pokedex.Resources.Funtranslations
{
    internal class TranslationRepository : ITranslationRepository
    {
        private readonly FuntranslationsClient _client;

        //In a production setting I would use an IDistributedCache (backed onto something like Redis) instead as it's very likely that there would be multiple load balanced instances of this API.
        private readonly IMemoryCache _cache;

        private readonly FuntranslationsOptions _options;
        private readonly ILogger<TranslationRepository> _logger;

        public TranslationRepository(FuntranslationsClient client, IMemoryCache cache, IOptions<FuntranslationsOptions> options, ILogger<TranslationRepository> logger)
        {
            _client = client;
            _cache = cache;
            _options = options.Value;
            _logger = logger;
        }

        //<inheritdoc />
        public async Task<Translation> GetTranslationAsync(string text, Language language)
        {
            //Check the cache for a value that we've already accessed. This has performance benefits as well as keeping licence-based request limits to a minimum.
            if (!_cache.TryGetValue($"{nameof(language)}_{text}", out Translation translation))
            {
                TranslationResponse response = null;
                try
                {
                    switch (language)
                    {
                        case Language.Shakespeare:
                            response = await _client.GetShakespeareTranslationAsync(text);
                            break;
                        case Language.Yoda:
                            response = await _client.GetYodaTranslationAsync(text);
                            break;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"An exception occurred while trying to translate to {nameof(language)} using the Funtranslations API.", e);
                    throw new TranslationUnavailableException(e);
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(_options.CacheExpiryInSeconds));

                translation = new Translation
                {
                    Language = language,
                    Text = response.Contents.Translated
                };

                _cache.Set($"{nameof(language)}_{text}", translation, cacheEntryOptions);
            }

            return translation;
        }
    }
}
