using Microsoft.EntityFrameworkCore;
using SchoolApi.Data;
using SchoolApi.Models;
using SchoolApi.Services.Interfaces;

namespace SchoolApi.Services.Implementations
{
    public class OgretmenService : IOgretmenService
    {
        private readonly ApplicationDbContext _db;

        public OgretmenService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Ogretmen>> GetAllAsync()
        {
            return await _db.Ogretmenler
                .Include(o => o.Dersler)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Ogretmen?> GetByIdAsync(int id)
        {
            return await _db.Ogretmenler
                .Include(o => o.Dersler)
                .AsNoTracking()
                .SingleOrDefaultAsync(o => o.Id == id);
        }

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
}