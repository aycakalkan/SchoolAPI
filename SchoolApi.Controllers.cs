// Controllers/SchoolControllers.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApi.Data;
using SchoolApi.Models;

namespace SchoolApi.Controllers;

/// <summary>
/// Temel CRUD işlemleri + ilişki yönetimi
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OgrencilerController(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;

    // GET api/ogrenciler
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ogrenci>>> GetAll() =>
        await _context.Ogrenciler
                      .Include(o => o.OgrenciDersler)
                          .ThenInclude(od => od.Ders)
                      .ToListAsync();

    // GET api/ogrenciler/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Ogrenci>> Get(int id)
    {
        var entity = await _context.Ogrenciler
                                   .Include(o => o.OgrenciDersler)
                                       .ThenInclude(od => od.Ders)
                                   .SingleOrDefaultAsync(o => o.Id == id);

        return entity is null ? NotFound() : entity;
    }

    // POST api/ogrenciler
    [HttpPost]
    public async Task<ActionResult<Ogrenci>> Create(Ogrenci ogrenci)
    {
        _context.Ogrenciler.Add(ogrenci);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = ogrenci.Id }, ogrenci);
    }

    // PUT api/ogrenciler/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, Ogrenci ogrenci)
    {
        if (id != ogrenci.Id) return BadRequest("Id alanı uyuşmuyor");
        _context.Entry(ogrenci).State = EntityState.Modified;

        try { await _context.SaveChangesAsync(); }
        catch (DbUpdateConcurrencyException) when (!EntityExists(id)) { return NotFound(); }

        return NoContent();
    }

    // DELETE api/ogrenciler/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.Ogrenciler.FindAsync(id);
        if (entity is null) return NotFound();

        _context.Ogrenciler.Remove(entity);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // --- many-to-many helpers ---------------------------------------------

    // POST api/ogrenciler/5/ders/3   (öğrenciyi derse ekle)
    [HttpPost("{ogrenciId:int}/ders/{dersId:int}")]
    public async Task<IActionResult> AddCourse(int ogrenciId, int dersId)
    {
        if (await _context.OgrenciDersler.FindAsync(ogrenciId, dersId) is not null)
            return Conflict("Öğrenci zaten bu derse kayıtlı.");

        _context.OgrenciDersler.Add(new OgrenciDers { OgrenciId = ogrenciId, DersId = dersId });
        await _context.SaveChangesAsync();
        return Ok();
    }

    // DELETE api/ogrenciler/5/ders/3  (öğrenciyi dersten çıkar)
    [HttpDelete("{ogrenciId:int}/ders/{dersId:int}")]
    public async Task<IActionResult> RemoveCourse(int ogrenciId, int dersId)
    {
        var link = await _context.OgrenciDersler.FindAsync(ogrenciId, dersId);
        if (link is null) return NotFound();

        _context.OgrenciDersler.Remove(link);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool EntityExists(int id) => _context.Ogrenciler.Any(e => e.Id == id);
}

// ---------------------------------------------------------------------------
// Dersler ( Courses ) Controller
// ---------------------------------------------------------------------------
[ApiController]
[Route("api/[controller]")]
public class DerslerController(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;

    [HttpGet]
    public async Task<IEnumerable<Ders>> Get() =>
        await _context.Dersler.Include(d => d.Ogretmen).ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Ders>> Get(int id) =>
        await _context.Dersler.Include(d => d.Ogretmen)
                              .SingleOrDefaultAsync(d => d.Id == id) is { } ders
            ? ders
            : NotFound();

    [HttpPost]
    public async Task<ActionResult<Ders>> Post(Ders ders)
    {
        _context.Dersler.Add(ders);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = ders.Id }, ders);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, Ders ders)
    {
        if (id != ders.Id) return BadRequest();
        _context.Entry(ders).State = EntityState.Modified;

        try { await _context.SaveChangesAsync(); }
        catch (DbUpdateConcurrencyException) when (!Exists(id)) { return NotFound(); }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.Dersler.FindAsync(id);
        if (entity is null) return NotFound();

        _context.Dersler.Remove(entity);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool Exists(int id) => _context.Dersler.Any(e => e.Id == id);
}

// ---------------------------------------------------------------------------
// Ogretmenler ( Teachers ) Controller
// ---------------------------------------------------------------------------
[ApiController]
[Route("api/[controller]")]
public class OgretmenlerController(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;

    [HttpGet]
    public async Task<IEnumerable<Ogretmen>> Get() =>
        await _context.Ogretmenler.Include(o => o.Dersler).ToListAsync();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Ogretmen>> Get(int id) =>
        await _context.Ogretmenler.Include(o => o.Dersler)
                                  .SingleOrDefaultAsync(o => o.Id == id) is { } t
            ? t
            : NotFound();

    [HttpPost]
    public async Task<ActionResult<Ogretmen>> Post(Ogretmen ogretmen)
    {
        _context.Ogretmenler.Add(ogretmen);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = ogretmen.Id }, ogretmen);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, Ogretmen ogretmen)
    {
        if (id != ogretmen.Id) return BadRequest();
        _context.Entry(ogretmen).State = EntityState.Modified;

        try { await _context.SaveChangesAsync(); }
        catch (DbUpdateConcurrencyException) when (!Exists(id)) { return NotFound(); }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.Ogretmenler.FindAsync(id);
        if (entity is null) return NotFound();

        _context.Ogretmenler.Remove(entity);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool Exists(int id) => _context.Ogretmenler.Any(e => e.Id == id);
}