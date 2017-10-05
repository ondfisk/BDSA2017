using BDSA2017.Lecture06.Entities;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BDSA2017.Lecture06.Tests
{
    public class CharacterRepositoryTests
    {
        [Fact]
        public async Task Delete_given_42_deletes_character_and_SaveChanges()
        {
            var character = new Character();
            var mock = new Mock<IFuturamaContext>();
            mock.Setup(m => m.Characters.FindAsync(42)).ReturnsAsync(character);
            mock.Setup(m => m.Characters.Remove(character));

            using (var repository = new CharacterRepository(mock.Object))
            {
                await repository.DeleteAsync(42);
            }

            mock.Verify(m => m.SaveChangesAsync(default(CancellationToken)));
        }
    }
}
