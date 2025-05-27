using Microsoft.EntityFrameworkCore;
using SchoolApi.Data;
using SchoolApi.Models;
using SchoolApi.Services.Interfaces;

namespace SchoolApi.Services.Implementations
{
    public class OgrenciDersService : IOgrenciDersService
    {
        private readonly ApplicationDbContext _db;

        public OgrenciDersService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> AddCourseAsync(int ogrenciId, int dersId)
        {
            // Check if the relationship already exists
            if (await _db.OgrenciDersler.FindAsync(ogrenciId, dersId) is not null)
                return false; // Already enrolled

            // Check if both student and course exist
            var ogrenciExists = await _db.Ogrenciler.AnyAsync(o => o.Id == ogrenciId);
            var dersExists = await _db.Dersler.AnyAsync(d => d.Id == dersId);

            if (!ogrenciExists || !dersExists)
                return false;

            _db.OgrenciDersler.Add(new OgrenciDers
            {
                OgrenciId = ogrenciId,
                DersId = dersId
            });

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