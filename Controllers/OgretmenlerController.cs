using Microsoft.AspNetCore.Mvc;
using SchoolApi.Models;
using SchoolApi.Services;
using SchoolApi.Services.Interfaces;

namespace SchoolApi.Controllers
{
    /// <summary>
    /// Öğretmen (Teacher) CRUD operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OgretmenlerController : ControllerBase
    {
        private readonly IOgretmenService _ogretmenService;

        public OgretmenlerController(IOgretmenService ogretmenService)
        {
            _ogretmenService = ogretmenService;
        }

        // GET: api/ogretmenler
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ogretmen>>> GetAll()
        {
            var ogretmenler = await _ogretmenService.GetAllAsync();
            return Ok(ogretmenler);
        }

        // GET: api/ogretmenler/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ogretmen>> Get(int id)
        {
            var ogretmen = await _ogretmenService.GetByIdAsync(id);
            if (ogretmen == null)
                return NotFound();

            return Ok(ogretmen);
        }

        // POST: api/ogretmenler
        [HttpPost]
        public async Task<ActionResult<Ogretmen>> Create([FromBody] Ogretmen ogretmen)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdOgretmen = await _ogretmenService.CreateAsync(ogretmen);
            return CreatedAtAction(nameof(Get), new { id = createdOgretmen.Id }, createdOgretmen);
        }

        // PUT: api/ogretmenler/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Ogretmen ogretmen)
        {
            if (id != ogretmen.Id)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _ogretmenService.UpdateAsync(ogretmen);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/ogretmenler/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _ogretmenService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}