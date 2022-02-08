namespace Pokedex.Resources.Funtranslations
{
    public class FuntranslationsOptions
    {
        public const string ConfigurationKey = "Funtranslations";
        /// <summary>
        /// The endpoint of the Funtranslations resource.
        /// </summary>
        public string Endpoint { get; set; }

        /// <summary>
        /// The API Key (optional) of the Funtranslations resource.
        /// </summary>
        public string APIKey { get; set; }

        /// <summary>
        /// The expiry time (in seconds) of a cache entry.
        /// </summary>
        public int CacheExpiryInSeconds { get; set; }
    }
}
