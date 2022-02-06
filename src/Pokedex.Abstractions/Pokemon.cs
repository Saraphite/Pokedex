using System;

namespace Pokedex.Abstractions
{
    public class Pokemon
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Habitat Habitat { get; set; }
        public bool IsLegendary { get; set; }
    }
}
