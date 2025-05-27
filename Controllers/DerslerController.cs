// Controllers/DerslerController.cs
using Microsoft.AspNetCore.Mvc;
using SchoolApi.Models;
using SchoolApi.Services;
using SchoolApi.Services.Interfaces;

namespace SchoolApi.Controllers
{
    /// <summary>
    /// Ders (Course) CRUD operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DerslerController : ControllerBase
    {
        private readonly IDersService _dersService;

        public DerslerController(IDersService dersService)
        {
            _dersService = dersService;
        }

        // GET: api/dersler
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ders>>> GetAll()
        {
            var dersler = await _dersService.GetAllAsync();
            return Ok(dersler);
        }

        // GET: api/dersler/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ders>> Get(int id)
        {
            var ders = await _dersService.GetByIdAsync(id);
            if (ders == null)
                return NotFound();

            return Ok(ders);
        }

        // POST: api/dersler
        [HttpPost]
        public async Task<ActionResult<Ders>> Create([FromBody] Ders ders)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdDers = await _dersService.CreateAsync(ders);
            return CreatedAtAction(nameof(Get), new { id = createdDers.Id }, createdDers);
        }

        // PUT: api/dersler/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Ders ders)
        {
            if (id != ders.Id)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _dersService.UpdateAsync(ders);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/dersler/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _dersService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}