
// Controllers/OgrencilerController.cs
using Microsoft.AspNetCore.Mvc;
using SchoolApi.Models;
using SchoolApi.Services;
using SchoolApi.Services.Interfaces;

namespace SchoolApi.Controllers
{
    /// <summary>
    /// Öğrenci (Student) CRUD operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OgrencilerController : ControllerBase
    {
        private readonly IOgrenciService _ogrenciService;
        private readonly IOgrenciDersService _ogrenciDersService;

        public OgrencilerController(IOgrenciService ogrenciService, IOgrenciDersService ogrenciDersService)
        {
            _ogrenciService = ogrenciService;
            _ogrenciDersService = ogrenciDersService;
        }

        // GET: api/ogrenciler
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ogrenci>>> GetAll()
        {
            var ogrenciler = await _ogrenciService.GetAllAsync();
            return Ok(ogrenciler);
        }

        // GET: api/ogrenciler/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ogrenci>> Get(int id)
        {
            var ogrenci = await _ogrenciService.GetByIdAsync(id);
            if (ogrenci == null)
                return NotFound();

            return Ok(ogrenci);
        }

        // POST: api/ogrenciler
        [HttpPost]
        public async Task<ActionResult<Ogrenci>> Create([FromBody] Ogrenci ogrenci)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdOgrenci = await _ogrenciService.CreateAsync(ogrenci);
            return CreatedAtAction(nameof(Get), new { id = createdOgrenci.Id }, createdOgrenci);
        }

        // PUT: api/ogrenciler/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Ogrenci ogrenci)
        {
            if (id != ogrenci.Id)
                return BadRequest("ID mismatch");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _ogrenciService.UpdateAsync(ogrenci);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/ogrenciler/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _ogrenciService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // POST: api/ogrenciler/5/ders/3
        [HttpPost("{ogrenciId}/ders/{dersId}")]
        public async Task<IActionResult> AddCourse(int ogrenciId, int dersId)
        {
            var success = await _ogrenciDersService.AddCourseAsync(ogrenciId, dersId);
            if (!success)
                return Conflict("Student is already enrolled in this course");

            return Ok();
        }

        // DELETE: api/ogrenciler/5/ders/3
        [HttpDelete("{ogrenciId}/ders/{dersId}")]
        public async Task<IActionResult> RemoveCourse(int ogrenciId, int dersId)
        {
            var success = await _ogrenciDersService.RemoveCourseAsync(ogrenciId, dersId);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}