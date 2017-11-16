using BDSA2017.Lecture10.Common;
using BDSA2017.Lecture10.Models;
using BDSA2017.Lecture10.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BDSA2017.Assignment10.Web.Tests
{
    public class CharactersControllerTests
    {
        [Fact(DisplayName = "Get returns Ok with characters")]
        public async Task Get_returns_Ok_with_characters()
        {
            var characters = new CharacterDTO[0];

            var repository = new Mock<ICharacterRepository>();
            repository.Setup(r => r.ReadAsync()).ReturnsAsync(characters);

            var controller = new CharactersController(repository.Object);

            var result = await controller.Get() as OkObjectResult;

            Assert.Equal(characters, result.Value);
        }

        [Fact(DisplayName = "Get given existing id returns Ok with character")]
        public async Task Get_given_existing_id_returns_Ok_with_character()
        {
            var character = new CharacterDetailsDTO();

            var repository = new Mock<ICharacterRepository>();
            repository.Setup(r => r.FindAsync(42)).ReturnsAsync(character);

            var controller = new CharactersController(repository.Object);

            var result = await controller.Get(42) as OkObjectResult;

            Assert.Equal(character, result.Value);
        }

        [Fact(DisplayName = "GetImage given existing id returns File with image")]
        public async Task GetImage_given_existing_id_returns_File_with_image()
        {
            var character = new CharacterDetailsDTO { Id = 42, Image = "foo.png" };

            var repository = new Mock<ICharacterRepository>();
            repository.Setup(r => r.FindAsync(42)).ReturnsAsync(character);

            var controller = new CharactersController(repository.Object);

            var result = await controller.GetImage(42) as VirtualFileResult;

            Assert.Equal("images/foo.png", result.FileName);
            Assert.Equal("image/png", result.ContentType);
        }

        [Fact(DisplayName = "GetImage given non existing id returns NotFound")]
        public async Task GetImage_given_non_existing_id_returns_NotFound()
        {
            var repository = new Mock<ICharacterRepository>();
            repository.Setup(r => r.FindAsync(42)).ReturnsAsync(default(CharacterDetailsDTO));

            var controller = new CharactersController(repository.Object);

            var result = await controller.GetImage(42);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact(DisplayName = "Get given non-existing id returns NotFound")]
        public async Task Get_given_non_existing_id_returns_NotFound()
        {
            var repository = new Mock<ICharacterRepository>();
            repository.Setup(r => r.FindAsync(42)).ReturnsAsync(default(CharacterDetailsDTO));

            var controller = new CharactersController(repository.Object);

            var result = await controller.Get(42);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact(DisplayName = "Post given invalid character returns BadRequest")]
        public async Task Post_given_invalid_character_returns_BadRequest()
        {
            var repository = new Mock<ICharacterRepository>();

            var controller = new CharactersController(repository.Object);
            controller.ModelState.AddModelError(string.Empty, "Error");

            var character = new CharacterCreateDTO();
            var result = await controller.Post(character);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact(DisplayName = "Post given invalid character does not call CreateAsync")]
        public async Task Post_given_invalid_character_does_not_call_CreateAsync()
        {
            var repository = new Mock<ICharacterRepository>();

            var controller = new CharactersController(repository.Object);
            controller.ModelState.AddModelError(string.Empty, "Error");

            var character = new CharacterCreateDTO();
            await controller.Post(character);

            repository.Verify(r => r.CreateAsync(It.IsAny<CharacterCreateDTO>()), Times.Never);
        }

        [Fact(DisplayName = "Post given valid character calls CreateAsync")]
        public async Task Post_given_valid_character_calls_CreateAsync()
        {
            var repository = new Mock<ICharacterRepository>();

            var controller = new CharactersController(repository.Object);

            var character = new CharacterCreateDTO();
            await controller.Post(character);

            repository.Verify(r => r.CreateAsync(character));
        }

        [Fact(DisplayName = "Post given valid character returns CreatedAtAction")]
        public async Task Post_given_valid_character_returns_CreatedAtAction()
        {
            var repository = new Mock<ICharacterRepository>();
            repository.Setup(r => r.CreateAsync(It.IsAny<CharacterCreateDTO>())).ReturnsAsync(42);
            var controller = new CharactersController(repository.Object);

            var character = new CharacterCreateDTO();
            var result = await controller.Post(character) as CreatedAtActionResult;

            Assert.Equal(nameof(CharactersController.Get), result.ActionName);
            Assert.Equal(42, result.RouteValues["id"]);
        }

        [Fact(DisplayName = "Put given invalid character returns BadRequest")]
        public async Task Put_given_invalid_character_returns_BadRequest()
        {
            var repository = new Mock<ICharacterRepository>();

            var controller = new CharactersController(repository.Object);
            controller.ModelState.AddModelError(string.Empty, "Error");

            var character = new CharacterUpdateDTO { Id = 42 };
            var result = await controller.Put(42, character);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact(DisplayName = "Put given id != character.Id returns BadRequest")]
        public async Task Put_given_id_not_equal_to_character_id_returns_BadRequest()
        {
            var repository = new Mock<ICharacterRepository>();

            var controller = new CharactersController(repository.Object);

            var customer = new CharacterUpdateDTO { Id = 42 };
            var result = await controller.Put(0, customer);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact(DisplayName = "Put given invalid character does not call UpdateAsync")]
        public async Task Put_given_invalid_character_does_not_call_UpdateAsync()
        {
            var repository = new Mock<ICharacterRepository>();

            var controller = new CharactersController(repository.Object);
            controller.ModelState.AddModelError(string.Empty, "Error");

            var character = new CharacterUpdateDTO();
            await controller.Put(42, character);

            repository.Verify(r => r.UpdateAsync(It.IsAny<CharacterUpdateDTO>()), Times.Never);
        }

        [Fact(DisplayName = "Put given valid character calls UpdateAsync")]
        public async Task Put_given_valid_character_calls_UpdateAsync()
        {
            var repository = new Mock<ICharacterRepository>();

            var controller = new CharactersController(repository.Object);

            var character = new CharacterUpdateDTO { Id = 42 };
            await controller.Put(42, character);

            repository.Verify(r => r.UpdateAsync(character));
        }

        [Fact(DisplayName = "Put given non-existing character returns NotFound")]
        public async Task Put_given_non_existing_character_returns_NotFound()
        {
            var repository = new Mock<ICharacterRepository>();
            repository.Setup(r => r.UpdateAsync(It.IsAny<CharacterUpdateDTO>())).ReturnsAsync(false);

            var controller = new CharactersController(repository.Object);

            var character = new CharacterUpdateDTO { Id = 42 };
            var result = await controller.Put(42, character);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact(DisplayName = "Put given valid character returns NoContent")]
        public async Task Put_given_valid_character_returns_NoContent()
        {
            var repository = new Mock<ICharacterRepository>();
            repository.Setup(r => r.UpdateAsync(It.IsAny<CharacterUpdateDTO>())).ReturnsAsync(true);

            var controller = new CharactersController(repository.Object);

            var character = new CharacterUpdateDTO { Id = 42 };
            var result = await controller.Put(42, character);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact(DisplayName = "Delete given non-existing character returns NotFound")]
        public async Task Delete_given_non_existing_character_returns_NotFound()
        {
            var repository = new Mock<ICharacterRepository>();
            repository.Setup(r => r.DeleteAsync(42)).ReturnsAsync(false);

            var controller = new CharactersController(repository.Object);

            var character = new CharacterUpdateDTO();
            var result = await controller.Delete(42);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact(DisplayName = "Delete given valid character returns NoContent")]
        public async Task Delete_given_valid_character_returns_NoContent()
        {
            var repository = new Mock<ICharacterRepository>();
            repository.Setup(r => r.DeleteAsync(42)).ReturnsAsync(true);

            var controller = new CharactersController(repository.Object);

            var character = new CharacterUpdateDTO();
            var result = await controller.Delete(42);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact(DisplayName = "Delete given id calls DeleteAsync")]
        public async Task Delete_given_id_calls_DeleteAsync()
        {
            var repository = new Mock<ICharacterRepository>();

            var controller = new CharactersController(repository.Object);

            await controller.Delete(42);

            repository.Verify(r => r.DeleteAsync(42));
        }
    }
}
