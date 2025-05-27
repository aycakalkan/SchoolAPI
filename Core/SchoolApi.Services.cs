// Services/SchoolServices.cs
using Microsoft.EntityFrameworkCore;
using SchoolApi.Data;
using SchoolApi.Models;

namespace SchoolApi.Services
{
    // ─────────────────────────── Öğrenci ───────────────────────────
    public interface IOgrenciService
    {
        Task<IEnumerable<Ogrenci>> GetAllAsync();
        Task<Ogrenci?> GetByIdAsync(int id);
        Task<Ogrenci> CreateAsync(Ogrenci ogrenci);
        Task<bool> UpdateAsync(Ogrenci ogrenci);
        Task<bool> DeleteAsync(int id);
    }

    public class OgrenciService(ApplicationDbContext db) : IOgrenciService
    {
        private readonly ApplicationDbContext _db = db;

        public async Task<IEnumerable<Ogrenci>> GetAllAsync() =>
            await _db.Ogrenciler
                     .Include(o => o.OgrenciDersler)
                         .ThenInclude(od => od.Ders)
                     .AsNoTracking()
                     .ToListAsync();

        public async Task<Ogrenci?> GetByIdAsync(int id) =>
            await _db.Ogrenciler
                     .Include(o => o.OgrenciDersler)
                         .ThenInclude(od => od.Ders)
                     .AsNoTracking()
                     .SingleOrDefaultAsync(o => o.Id == id);

        public async Task<Ogrenci> CreateAsync(Ogrenci ogrenci)
        {
            _db.Ogrenciler.Add(ogrenci);
            await _db.SaveChangesAsync();
            return ogrenci;
        }

        public async Task<bool> UpdateAsync(Ogrenci ogrenci)
        {
            if (!await _db.Ogrenciler.AnyAsync(o => o.Id == ogrenci.Id))
                return false;

            _db.Entry(ogrenci).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _db.Ogrenciler.FindAsync(id);
            if (entity is null) return false;

            _db.Ogrenciler.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }
    }

    // ─────────────────────────── Ders ──────────────────────────────
    public interface IDersService
    {
        Task<IEnumerable<Ders>> GetAllAsync();
        Task<Ders?> GetByIdAsync(int id);
        Task<Ders> CreateAsync(Ders ders);
        Task<bool> UpdateAsync(Ders ders);
        Task<bool> DeleteAsync(int id);
    }

    public class DersService(ApplicationDbContext db) : IDersService
    {
        private readonly ApplicationDbContext _db = db;

        public async Task<IEnumerable<Ders>> GetAllAsync() =>
            await _db.Dersler
                     .Include(d => d.Ogretmen)
                     .Include(d => d.OgrenciDersler).ThenInclude(od => od.Ogrenci)
                     .AsNoTracking()
                     .ToListAsync();

        public async Task<Ders?> GetByIdAsync(int id) =>
            await _db.Dersler
                     .Include(d => d.Ogretmen)
                     .Include(d => d.OgrenciDersler).ThenInclude(od => od.Ogrenci)
                     .AsNoTracking()
                     .SingleOrDefaultAsync(d => d.Id == id);

        public async Task<Ders> CreateAsync(Ders ders)
        {
            _db.Dersler.Add(ders);
            await _db.SaveChangesAsync();
            return ders;
        }

        public async Task<bool> UpdateAsync(Ders ders)
        {
            if (!await _db.Dersler.AnyAsync(d => d.Id == ders.Id))
                return false;

            _db.Entry(ders).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _db.Dersler.FindAsync(id);
            if (entity is null) return false;

            _db.Dersler.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }
    }

    // ─────────────────────────── Öğretmen ──────────────────────────
    public interface IOgretmenService
    {
        Task<IEnumerable<Ogretmen>> GetAllAsync();
        Task<Ogretmen?> GetByIdAsync(int id);
        Task<Ogretmen> CreateAsync(Ogretmen ogretmen);
        Task<bool> UpdateAsync(Ogretmen ogretmen);
        Task<bool> DeleteAsync(int id);
    }

    public class OgretmenService(ApplicationDbContext db) : IOgretmenService
    {
        private readonly ApplicationDbContext _db = db;

        public async Task<IEnumerable<Ogretmen>> GetAllAsync() =>
            await _db.Ogretmenler
                     .Include(o => o.Dersler)
                     .AsNoTracking()
                     .ToListAsync();

        public async Task<Ogretmen?> GetByIdAsync(int id) =>
            await _db.Ogretmenler
                     .Include(o => o.Dersler)
                     .AsNoTracking()
                     .SingleOrDefaultAsync(o => o.Id == id);

        public async Task<Ogretmen> CreateAsync(Ogretmen ogretmen)
        {
            _db.Ogretmenler.Add(ogretmen);
            await _db.SaveChangesAsync();
            return ogretmen;
        }

        public async Task<bool> UpdateAsync(Ogretmen ogretmen)
        {
            if (!await _db.Ogretmenler.AnyAsync(o => o.Id == ogretmen.Id))
                return false;

            _db.Entry(ogretmen).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _db.Ogretmenler.FindAsync(id);
            if (entity is null) return false;

            _db.Ogretmenler.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }
    }

    // ─────────── Öğrenci ↔ Ders (many-to-many yardımcı) ────────────
    public interface IOgrenciDersService
    {
        Task<bool> AddCourseAsync(int ogrenciId, int dersId);
        Task<bool> RemoveCourseAsync(int ogrenciId, int dersId);
    }

    public class OgrenciDersService(ApplicationDbContext db) : IOgrenciDersService
    {
        private readonly ApplicationDbContext _db = db;

        public async Task<bool> AddCourseAsync(int ogrenciId, int dersId)
        {
            if (await _db.OgrenciDersler.FindAsync(ogrenciId, dersId) is not null)
                return false;                 // zaten kayıtlı

            _db.OgrenciDersler.Add(new OgrenciDers { OgrenciId = ogrenciId, DersId = dersId });
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveCourseAsync(int ogrenciId, int dersId)
        {
            var link = await _db.OgrenciDersler.FindAsync(ogrenciId, dersId);
            if (link is null) return false;

            _db.OgrenciDersler.Remove(link);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}