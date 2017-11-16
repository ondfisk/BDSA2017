using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BDSA2017.Lecture10.Models;
using BDSA2017.Lecture10.Common;
using Microsoft.AspNetCore.Authorization;

namespace BDSA2017.Lecture10.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CharactersController : Controller
    {
        private readonly ICharacterRepository _repository;

        public CharactersController(ICharacterRepository repository)
        {
            _repository = repository;
        }

        // GET: api/characters
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var characters = await _repository.ReadAsync();

            return Ok(characters);
        }

        // GET: api/characters/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var character = await _repository.FindAsync(id);

            if (character == null)
            {
                return NotFound();
            }

            return Ok(character);
        }

        // GET: api/characters/5/image
        [HttpGet("{id}/image")]
        public async Task<IActionResult> GetImage(int id)
        {
            var character = await _repository.FindAsync(id);

            if (character?.Image == null)
            {
                return NotFound();
            }

            return File($"images/{character.Image}", "image/png");
        }

        // POST: api/characters
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CharacterCreateDTO character)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = await _repository.CreateAsync(character);

            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        // PUT: api/characters/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]CharacterUpdateDTO character)
        {
            if (id != character.Id)
            {
                ModelState.AddModelError(string.Empty, "id must match character.id");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await _repository.UpdateAsync(character);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/characters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
