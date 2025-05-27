using Microsoft.EntityFrameworkCore;
using SchoolApi.Data;
using SchoolApi.Models;
using SchoolApi.Services.Interfaces;

namespace SchoolApi.Services.Implementations
{
    public class DersService : IDersService
    {
        private readonly ApplicationDbContext _db;

        public DersService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Ders>> GetAllAsync()
        {
            return await _db.Dersler
                .Include(d => d.Ogretmen)
                .Include(d => d.OgrenciDersler)
                    .ThenInclude(od => od.Ogrenci)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Ders?> GetByIdAsync(int id)
        {
            return await _db.Dersler
                .Include(d => d.Ogretmen)
                .Include(d => d.OgrenciDersler)
                    .ThenInclude(od => od.Ogrenci)
                .AsNoTracking()
                .SingleOrDefaultAsync(d => d.Id == id);
        }

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
}