using Microsoft.EntityFrameworkCore;
using SchoolApi.Data;
using SchoolApi.Models;
using SchoolApi.Services.Interfaces;

namespace SchoolApi.Services.Implementations
{
    public class OgrenciService : IOgrenciService
    {
        private readonly ApplicationDbContext _db;

        public OgrenciService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Ogrenci>> GetAllAsync()
        {
            return await _db.Ogrenciler
                .Include(o => o.OgrenciDersler)
                    .ThenInclude(od => od.Ders)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Ogrenci?> GetByIdAsync(int id)
        {
            return await _db.Ogrenciler
                .Include(o => o.OgrenciDersler)
                    .ThenInclude(od => od.Ders)
                .AsNoTracking()
                .SingleOrDefaultAsync(o => o.Id == id);
        }

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
}