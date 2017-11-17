using BDSA2017.Lecture11.Common;
using BDSA2017.Lecture11.Entities;
using BDSA2017.Lecture11.Web.Model;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BDSA2017.Lecture11.Web.Tests.Model
{
    public class EntityFrameworkCharacterRepositoryTests
    {
        [Fact]
        public async Task CreateAsync_given_character_adds_it()
        {
            var entity = default(Character);
            var context = new Mock<IFuturamaContext>();
            context.Setup(c => c.Characters.Add(It.IsAny<Character>())).Callback<Character>(t => entity = t);

            using (var repository = new EntityFrameworkCharacterRepository(context.Object))
            {
                var character = new CharacterCreateDTO
                {
                    ActorId = 1,
                    Name = "Name",
                    Species = "Species",
                    Planet = "Planet",
                    Image = "Image"
                };
                await repository.CreateAsync(character);
            }

            Assert.Equal(1, entity.ActorId);
            Assert.Equal("Name", entity.Name);
            Assert.Equal("Species", entity.Species);
            Assert.Equal("Planet", entity.Planet);
            Assert.Equal("Image", entity.Image);
        }

        [Fact]
        public async Task Create_given_character_calls_SaveChangesAsync()
        {
            var context = new Mock<IFuturamaContext>();
            context.Setup(c => c.Characters.Add(It.IsAny<Character>()));

            using (var repository = new EntityFrameworkCharacterRepository(context.Object))
            {
                var character = new CharacterCreateDTO
                {
                    Name = "Name",
                    Species = "Species",
                };
                await repository.CreateAsync(character);
            }

            context.Verify(c => c.SaveChangesAsync(default(CancellationToken)));
        }

        [Fact]
        public async Task Create_given_character_returns_new_Id()
        {
            var entity = default(Character);

            var context = new Mock<IFuturamaContext>();
            context.Setup(c => c.Characters.Add(It.IsAny<Character>()))
                .Callback<Character>(t => entity = t);
            context.Setup(c => c.SaveChangesAsync(default(CancellationToken)))
                .Returns(Task.FromResult(0))
                .Callback(() => entity.Id = 42);

            using (var repository = new EntityFrameworkCharacterRepository(context.Object))
            {
                var character = new CharacterCreateDTO
                {
                    Name = "Name",
                    Species = "Species",
                };
                var id = await repository.CreateAsync(character);

                Assert.Equal(42, id);
            }
        }

        [Fact]
        public async Task Find_given_non_existing_id_returns_null()
        {
            var builder = new DbContextOptionsBuilder<FuturamaContext>()
                              .UseInMemoryDatabase(nameof(Find_given_non_existing_id_returns_null));

            using (var context = new FuturamaContext(builder.Options))
            using (var repository = new EntityFrameworkCharacterRepository(context))
            {
                var character = await repository.FindAsync(42);

                Assert.Null(character);
            }
        }

        [Fact]
        public async Task Find_given_existing_id_returns_mapped_CharacterDTO()
        {
            using (var connection = new SqliteConnection("DataSource=:memory:"))
            {
                connection.Open();

                var builder = new DbContextOptionsBuilder<FuturamaContext>()
                                  .UseSqlite(connection);

                var context = new FuturamaContext(builder.Options);
                await context.Database.EnsureCreatedAsync();

                var entity = new Character
                {
                    Name = "Name",
                    Species = "Species",
                    Planet = "Planet",
                    Image = "Image",
                    Actor = new Actor { Name = "Actor" },
                    Episodes = new[] { new EpisodeCharacter { Episode = new Episode { Title = "Episode 1" } }, new EpisodeCharacter { Episode = new Episode { Title = "Episode 2" } } }
                };

                context.Characters.Add(entity);
                await context.SaveChangesAsync();
                var id = entity.Id;

                using (var repository = new EntityFrameworkCharacterRepository(context))
                {
                    var character = await repository.FindAsync(id);

                    Assert.Equal("Name", character.Name);
                    Assert.Equal("Species", character.Species);
                    Assert.Equal("Planet", character.Planet);
                    Assert.Equal("Image", character.Image);
                    Assert.Equal("Actor", character.ActorName);
                    Assert.Equal(2, character.NumberOfEpisodes);
                }
            }
        }

        [Fact]
        public async Task Read_returns_mapped_CharacterDTO()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var builder = new DbContextOptionsBuilder<FuturamaContext>()
                              .UseSqlite(connection);

            var context = new FuturamaContext(builder.Options);
            context.Database.EnsureCreated();

            var entity = new Character
            {
                Name = "Name",
                Species = "Species",
                Actor = new Actor { Name = "Actor" }
            };

            context.Characters.Add(entity);
            await context.SaveChangesAsync();
            var id = entity.Id;

            using (var repository = new EntityFrameworkCharacterRepository(context))
            {
                var characters = await repository.ReadAsync();
                var character = characters.Single();

                Assert.Equal("Name", character.Name);
                Assert.Equal("Actor", character.ActorName);
            }
        }

        [Fact]
        public async Task Update_given_existing_character_Updates_properties()
        {
            var context = new Mock<IFuturamaContext>();
            var entity = new Character { Id = 42 };
            context.Setup(c => c.Characters.FindAsync(42)).ReturnsAsync(entity);

            using (var repository = new EntityFrameworkCharacterRepository(context.Object))
            {
                var character = new CharacterUpdateDTO
                {
                    Id = 42,
                    ActorId = 12,
                    Name = "Name",
                    Species = "Species",
                    Planet = "Planet",
                    Image = "Image"
                };

                await repository.UpdateAsync(character);
            }

            Assert.Equal(12, entity.ActorId);
            Assert.Equal("Name", entity.Name);
            Assert.Equal("Species", entity.Species);
            Assert.Equal("Planet", entity.Planet);
            Assert.Equal("Image", entity.Image);
        }

        [Fact]
        public async Task Update_given_existing_character_calls_SaveChangesAsync()
        {
            var context = new Mock<IFuturamaContext>();
            var entity = new Character { Id = 42 };
            context.Setup(c => c.Characters.FindAsync(42)).ReturnsAsync(entity);

            using (var repository = new EntityFrameworkCharacterRepository(context.Object))
            {
                var character = new CharacterUpdateDTO
                {
                    Id = 42,
                    Name = "Name",
                    Species = "Species",
                };

                await repository.UpdateAsync(character);
            }

            context.Verify(c => c.SaveChangesAsync(default(CancellationToken)));
        }

        [Fact]
        public async Task Update_given_existing_character_returns_true()
        {
            var context = new Mock<IFuturamaContext>();
            var entity = new Character { Id = 42 };
            context.Setup(c => c.Characters.FindAsync(42)).ReturnsAsync(entity);

            using (var repository = new EntityFrameworkCharacterRepository(context.Object))
            {
                var character = new CharacterUpdateDTO
                {
                    Id = 42,
                    Name = "Name",
                    Species = "Species",
                };

                var result = await repository.UpdateAsync(character);

                Assert.True(result);
            }
        }

        [Fact]
        public async Task Update_given_non_existing_character_returns_false()
        {
            var context = new Mock<IFuturamaContext>();
            context.Setup(c => c.Characters.FindAsync(42)).ReturnsAsync(default(Character));

            using (var repository = new EntityFrameworkCharacterRepository(context.Object))
            {
                var character = new CharacterUpdateDTO
                {
                    Id = 42,
                    Name = "Name",
                    Species = "Species",
                };

                var result = await repository.UpdateAsync(character);

                Assert.False(result);
            }
        }

        [Fact]
        public async Task Update_given_non_existing_character_does_not_SaveChangesAsync()
        {
            var context = new Mock<IFuturamaContext>();
            context.Setup(c => c.Characters.FindAsync(42)).ReturnsAsync(default(Character));

            using (var repository = new EntityFrameworkCharacterRepository(context.Object))
            {
                var character = new CharacterUpdateDTO
                {
                    Id = 42,
                    Name = "Name",
                    Species = "Species",
                };

                await repository.UpdateAsync(character);
            }

            context.Verify(c => c.SaveChangesAsync(default(CancellationToken)), Times.Never);
        }

        [Fact]
        public async Task Delete_given_existing_character_removes_it()
        {
            var character = new Character();
            var mock = new Mock<IFuturamaContext>();
            mock.Setup(m => m.Characters.FindAsync(42)).ReturnsAsync(character);

            using (var repository = new EntityFrameworkCharacterRepository(mock.Object))
            {
                await repository.DeleteAsync(42);
            }

            mock.Verify(m => m.Characters.Remove(character));
        }

        [Fact]
        public async Task Delete_given_existing_character_calls_SaveChangesAsync()
        {
            var character = new Character();
            var mock = new Mock<IFuturamaContext>();
            mock.Setup(m => m.Characters.FindAsync(42)).ReturnsAsync(character);

            using (var repository = new EntityFrameworkCharacterRepository(mock.Object))
            {
                await repository.DeleteAsync(42);
            }

            mock.Verify(m => m.SaveChangesAsync(default(CancellationToken)));
        }

        [Fact]
        public async Task Delete_given_non_existing_character_does_not_remove_it()
        {
            var character = new Character();
            var mock = new Mock<IFuturamaContext>();
            mock.Setup(m => m.Characters.FindAsync(42)).ReturnsAsync(default(Character));

            using (var repository = new EntityFrameworkCharacterRepository(mock.Object))
            {
                await repository.DeleteAsync(42);
            }

            mock.Verify(m => m.Characters.Remove(character), Times.Never);
        }

        [Fact]
        public async Task Delete_given_non_existing_character_does_not_call_SaveChangesAsync()
        {
            var character = new Character();
            var mock = new Mock<IFuturamaContext>();
            mock.Setup(m => m.Characters.FindAsync(42)).ReturnsAsync(default(Character));

            using (var repository = new EntityFrameworkCharacterRepository(mock.Object))
            {
                await repository.DeleteAsync(42);
            }

            mock.Verify(m => m.SaveChangesAsync(default(CancellationToken)), Times.Never);
        }

        [Fact]
        public void Dispose_disposes_context()
        {
            var mock = new Mock<IFuturamaContext>();

            using (var repository = new EntityFrameworkCharacterRepository(mock.Object))
            {
            }

            mock.Verify(m => m.Dispose());
        }
    }
}
