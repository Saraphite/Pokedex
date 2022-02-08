namespace Pokedex.Resources.Funtranslations
{
    public class FuntranslationsOptions
    {
        public const string ConfigurationKey = "Funtranslations";
        public string Endpoint { get; set; }
        public string APIKey { get; set; }
        public int CacheExpiryInSeconds { get; set; }
    }
}
