using PokeApiNet;
using Pokedex.Abstractions;
using System;

namespace Pokedex.Resources.PokeAPI
{
    internal static class MappingExtensions
    {
        public static Habitat ToHabitat(this PokemonHabitat habitat)
        {
            switch (habitat.Name)
            {
                case "cave":
                    return Habitat.Cave;
                case "forest":
                    return Habitat.Forest;
                case "grassland":
                    return Habitat.Grassland;
                case "mountain":
                    return Habitat.Mountain;
                case "rare":
                    return Habitat.Rare;
                case "rough-terrain":
                    return Habitat.RoughTerrain;
                case "sea":
                    return Habitat.Sea;
                case "urban":
                    return Habitat.Urban;
                case "waters-edge":
                    return Habitat.WatersEdge;
                default:
                    return Habitat.Unknown;
            }
        }
    }
}
