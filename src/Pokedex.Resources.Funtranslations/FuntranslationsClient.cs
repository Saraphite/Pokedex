using Microsoft.Extensions.Options;
using Pokedex.Resources.Funtranslations.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pokedex.Resources.Funtranslations
{
    public class FuntranslationsClient
    {
        private readonly HttpClient _httpClient;
        private readonly FuntranslationsOptions _options;

        public FuntranslationsClient(HttpClient httpClient, IOptions<FuntranslationsOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;

            _httpClient.BaseAddress = new Uri(_options.Endpoint);
            //APIKey would be populated in a non-personal setting.
            if (!string.IsNullOrEmpty(_options.APIKey))
            {
                _httpClient.DefaultRequestHeaders.Add("X-Funtranslations-Api-Secret", _options.APIKey);
            }
        }

        public async Task<TranslationResponse> GetShakespeareTranslationAsync(string text)
        {
            var values = new Dictionary<string, string>()
            {
                { "text", text }
            };

            var content = new FormUrlEncodedContent(values);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            var response = await _httpClient.PostAsync(Methods.Shakespeare, content);

            return JsonSerializer.Deserialize<TranslationResponse>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<TranslationResponse> GetYodaTranslationAsync(string text)
        {
            var values = new Dictionary<string, string>()
            {
                { "text", text }
            };

            var content = new FormUrlEncodedContent(values);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            var response = await _httpClient.PostAsync(Methods.Yoda, content);

            return JsonSerializer.Deserialize<TranslationResponse>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        private struct Methods
        {
            public const string Shakespeare = "shakespeare.json";
            public const string Yoda = "yoda.json";
        }
    }
}
