using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BDSA2017.Lecture07.Models;

namespace BDSA2017.Lecture07.Web.Controllers
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

        // GET: api/Characters
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Characters/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            var character = await _repository.FindAsync(id);

            if (character == null)
            {
                return NotFound();
            }

            return Ok(character);
        }
        
        // POST: api/Characters
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CharacterCreateUpdateDTO character)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = await _repository.CreateAsync(character);

            return CreatedAtAction(nameof(Get), new { id }, null);
        }
        
        // PUT: api/Characters/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
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
