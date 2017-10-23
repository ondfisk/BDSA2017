using BDSA2017.Lecture08.Lib.Animals;
using Moq;
using Xunit;

namespace BDSA2017.Lecture08.Lib.Tests.Services
{
    public class AnimalServiceTests
    {
        [Fact]
        public void Speak_calls_animal_Hello()
        {
            var mock = new Mock<IAnimal>();

            var service = new AnimalService(mock.Object);

            service.Speak();

            mock.Verify(a => a.Hello(), Times.Once);
        }
    }
}
