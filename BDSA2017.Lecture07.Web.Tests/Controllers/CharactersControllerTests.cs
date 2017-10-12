using BDSA2017.Lecture07.Models;
using BDSA2017.Lecture07.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BDSA2017.Lecture07.Web.Tests.Controllers
{
    public class CharactersControllerTests
    {
        [Fact]
        public async Task Get_given_no_existing_id_returns_NotFound()
        {
            var repository = new Mock<ICharacterRepository>();
            repository.Setup(r => r.FindAsync(42)).ReturnsAsync(default(CharacterDTO));

            var controller = new CharactersController(repository.Object);

            var response = await controller.Get(42);

            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async Task Get_given_existing_id_returns_OK_with_character()
        {
            var character = new CharacterDTO { Id = 42 };
            var repository = new Mock<ICharacterRepository>();
            repository.Setup(r => r.FindAsync(42)).ReturnsAsync(character);

            var controller = new CharactersController(repository.Object);

            var response = await controller.Get(42) as OkObjectResult;

            Assert.Equal(character, response.Value);
        }

        [Fact]
        public async Task Create_given_character_returns_CreateAtAction()
        {
            var character = new CharacterCreateUpdateDTO { Name = "New guy", Species = "Human" };
            var repository = new Mock<ICharacterRepository>();
            repository.Setup(r => r.CreateAsync(character)).ReturnsAsync(42);

            var controller = new CharactersController(repository.Object);

            var response = await controller.Post(character) as CreatedAtActionResult;

            Assert.Equal("Get", response.ActionName);
            Assert.Equal(42, response.RouteValues["id"]);
        }

        [Fact]
        public async Task Create_given_invalid_character_returns_BadRequest()
        {
            var character = new CharacterCreateUpdateDTO();
            var repository = new Mock<ICharacterRepository>();

            var controller = new CharactersController(repository.Object);
            controller.ModelState.AddModelError(string.Empty, "Error");

            var response = await controller.Post(character);

            Assert.IsType<BadRequestObjectResult>(response);
        }
    }
}
