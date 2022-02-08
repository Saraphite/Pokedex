using System.Text.Json.Serialization;

namespace Pokedex.Abstractions
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Habitat Habitat { get; set; }
        public bool IsLegendary { get; set; }
    }
}
