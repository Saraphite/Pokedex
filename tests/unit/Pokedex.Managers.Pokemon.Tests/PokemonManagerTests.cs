using Microsoft.Extensions.Logging;
using NSubstitute;
using Pokedex.Data.Pokemon;
using Pokedex.Data.Pokemon.Exceptions;
using Pokedex.Data.Translations;
using Pokedex.Data.Translations.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace Pokedex.Managers.Pokemon.Tests
{
    public class PokemonManagerTests
    {
        PokemonManager _subject;
        IPokemonRepository _pokemonRepository;
        ITranslationRepository _translationRepository;

        Abstractions.Pokemon BasicPokemon = new Abstractions.Pokemon
        {
            Name = "pikachu",
            Id = 25,
            Description = "When several of these Pokémon gather, their electricity could build and cause lightning storms.",
            Habitat = Abstractions.Habitat.Forest,
            IsLegendary = false
        };

        Abstractions.Pokemon CavePokemon = new Abstractions.Pokemon
        {
            Name = "zubat",
            Id = 41,
            Description = "Forms colonies in perpetually dark places. Uses ultrasonic waves to identify and approach targets.",
            Habitat = Abstractions.Habitat.Cave,
            IsLegendary = false
        };

        Abstractions.Pokemon LegendaryPokemon = new Abstractions.Pokemon
        {
            Name = "mewtwo",
            Id = 150,
            Description = "It was created by a scientist after years of horrific gene splicing and DNA engineering experiments.",
            Habitat = Abstractions.Habitat.Rare,
            IsLegendary = true
        };

        Translation ShakespeareTranslation = new Translation
        {
            Language = Language.Shakespeare,
            Text = "This is a Shakespeare Translation."
        };

        Translation YodaTranslation = new Translation
        {
            Language = Language.Yoda,
            Text = "This is a Yoda Translation."
        };

        public PokemonManagerTests()
        {
            _pokemonRepository = Substitute.For<IPokemonRepository>();
            _translationRepository = Substitute.For<ITranslationRepository>();
            _subject = new PokemonManager(_pokemonRepository, _translationRepository, Substitute.For<ILogger<PokemonManager>>());
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_Pokemon()
        {
            _pokemonRepository.GetAsync(Arg.Any<string>()).Returns(BasicPokemon);

            var result = await _subject.GetPokemonAsync("pikachu");

            Assert.Equal(BasicPokemon.Id, result.Id);
            Assert.Equal(BasicPokemon.Name, result.Name);
            Assert.Equal(BasicPokemon.Habitat, result.Habitat);
            Assert.Equal(BasicPokemon.IsLegendary, result.IsLegendary);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_Nonexistent_Pokemon_Returns_Null()
        {
            _pokemonRepository.GetAsync(Arg.Any<string>()).Returns((Abstractions.Pokemon)null);

            var result = await _subject.GetPokemonAsync("agumon");

            Assert.Null(result);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_Translated_Nonexistent_Pokemon_Returns_Null()
        {
            _pokemonRepository.GetAsync(Arg.Any<string>()).Returns((Abstractions.Pokemon)null);

            var result = await _subject.GetTranslatedPokemonAsync("agumon");

            Assert.Null(result);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_Translated_Non_Legendary_Pokemon_With_Non_Cave_Habitat_Returns_Shakespeare_Description()
        {
            _pokemonRepository.GetAsync(Arg.Any<string>()).Returns(BasicPokemon);
            _translationRepository.GetTranslationAsync(Arg.Any<string>(), Language.Shakespeare).Returns(ShakespeareTranslation);

            var result = await _subject.GetTranslatedPokemonAsync("pikachu");

            Assert.Equal(BasicPokemon.Id, result.Id);
            Assert.Equal(BasicPokemon.Description, ShakespeareTranslation.Text);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_Translated_Pokemon_With_Cave_Habitat_Returns_Yoda_Description()
        {
            _pokemonRepository.GetAsync(Arg.Any<string>()).Returns(CavePokemon);
            _translationRepository.GetTranslationAsync(Arg.Any<string>(), Language.Yoda).Returns(YodaTranslation);

            var result = await _subject.GetTranslatedPokemonAsync("zubat");

            Assert.Equal(CavePokemon.Id, result.Id);
            Assert.Equal(CavePokemon.Description, YodaTranslation.Text);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_Translated_Legendary_Pokemon_Returns_Yoda_Description()
        {
            _pokemonRepository.GetAsync(Arg.Any<string>()).Returns(LegendaryPokemon);
            _translationRepository.GetTranslationAsync(Arg.Any<string>(), Language.Yoda).Returns(YodaTranslation);

            var result = await _subject.GetTranslatedPokemonAsync("mewtwo");

            Assert.Equal(LegendaryPokemon.Id, result.Id);
            Assert.Equal(LegendaryPokemon.Description, YodaTranslation.Text);
            Assert.Equal(LegendaryPokemon.IsLegendary, result.IsLegendary);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_Translated_Pokemon_Returns_Standard_Description_If_Translation_Unavailable()
        {
            _pokemonRepository.GetAsync(Arg.Any<string>()).Returns(BasicPokemon);
            _translationRepository.GetTranslationAsync(Arg.Any<string>(), Arg.Any<Language>()).Returns(Task.FromException<Translation>(new TranslationUnavailableException(new System.Exception())));

            var result = await _subject.GetTranslatedPokemonAsync("pikachu");

            Assert.Equal(BasicPokemon.Description, result.Description);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public async Task Get_Pokemon_Throws_Exception_On_Dependency_Failure()
        {
            _pokemonRepository.GetAsync(Arg.Any<string>()).Returns(Task.FromException<Abstractions.Pokemon>(new PokemonUnavailableException(new System.Exception())));

            await Assert.ThrowsAsync<PokemonUnavailableException>(async () => await _subject.GetPokemonAsync("pikachu"));
        }
    }
}
