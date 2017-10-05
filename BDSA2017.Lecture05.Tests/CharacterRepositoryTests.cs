using BDSA2017.Lecture05.Entities;
using BDSA2017.Lecture05.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BDSA2017.Lecture05.Tests
{
    public class CharacterRepositoryTests : IDisposable
    {
        private readonly FuturamaContext _context;

        public CharacterRepositoryTests()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var builder = new DbContextOptionsBuilder<FuturamaContext>()
                              .UseSqlite(connection);

            _context = new FuturamaContext(builder.Options);
            _context.Database.EnsureCreated();

            var actor = new Actor { Name = "Billy West" };
            var episode = new Episode { Title = "Episode no. 1" };
            var episodeCharacter = new EpisodeCharacter();
            var episodes = new[] { episodeCharacter };
            var character = new Character { Name = "Fry", Species = "Human", Actor = actor, Episodes = episodes };
            episodeCharacter.Characters = character;
            episodeCharacter.Episode = episode;
            _context.Characters.Add(character);
            _context.SaveChanges();
        }

        [Fact]
        public void Find_given_42_returns_character()
        {
            using (var repository = new CharacterRepository(_context))
            {
                var dto = repository.Find(1);

                Assert.Equal("Fry", dto.Name);
                Assert.Equal("Billy West", dto.ActorName);
                Assert.Equal(1, dto.NumberOfEpisodes);
            }
        }

        [Fact]
        public void Read_returns_mapped_characters()
        {
            using (var repository = new CharacterRepository(_context))
            {
                var dtos = repository.Read();
                var dto = dtos.Single();

                Assert.Equal("Fry", dto.Name);
                Assert.Equal("Billy West", dto.ActorName);
                Assert.Equal(1, dto.NumberOfEpisodes);
            }
        }

        [Fact]
        public void Create_given_a_character_it_maps_to_character()
        {
            var character = default(Character);
            var dto = new CharacterCreateUpdateDTO
            {
                ActorId = 42,
                Name = "Turange Leela",
                Species = "Mutant, Human",
                Planet = "Earth"
            };

            var mock = new Mock<IFuturamaContext>();
            mock.Setup(s => s.Characters.Add(It.IsAny<Character>()))
                             .Callback<Character>(c => character = c);

            using (var repository = new CharacterRepository(mock.Object))
            {
                repository.Create(dto);
            }

            Assert.Equal(42, character.ActorId);
            Assert.Equal("Turange Leela", character.Name);
            Assert.Equal("Mutant, Human", character.Species);
            Assert.Equal("Earth", character.Planet);
        }

        [Fact]
        public void Dispose_calls_Dispose_on_context()
        {
            var mock = new Mock<IFuturamaContext>();

            using (var repository = new CharacterRepository(mock.Object))
            {
            }

            mock.Verify(c => c.Dispose());
        }

        [Fact]
        public void Delete_given_42_deletes_character_and_SaveChanges()
        {
            var character = new Character();
            var mock = new Mock<IFuturamaContext>();
            mock.Setup(m => m.Characters.Find(42)).Returns(character);
            mock.Setup(m => m.Characters.Remove(character));

            using (var repository = new CharacterRepository(mock.Object))
            {
                repository.Delete(42);
            }

            mock.Verify(m => m.SaveChanges());
        }

        [Fact]
        public void Delete_given_an_existing_character_it_returns_true()
        {
            var character = new Character();
            var mock = new Mock<IFuturamaContext>();
            mock.Setup(m => m.Characters.Find(42)).Returns(character);
            mock.Setup(m => m.Characters.Remove(character));

            using (var repository = new CharacterRepository(mock.Object))
            {
                var result = repository.Delete(42);

                Assert.True(result);
            }
        }

        [Fact]
        public void Delete_given_a_non_existing_character_it_returns_false()
        {
            var mock = new Mock<IFuturamaContext>();
            mock.Setup(m => m.Characters.Find(42)).Returns(default(Character));

            using (var repository = new CharacterRepository(mock.Object))
            {
                var result = repository.Delete(42);

                Assert.False(result);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
