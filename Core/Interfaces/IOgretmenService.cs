using SchoolApi.Models;

namespace SchoolApi.Services.Interfaces
{
    public interface IOgretmenService
    {
        Task<IEnumerable<Ogretmen>> GetAllAsync();
        Task<Ogretmen?> GetByIdAsync(int id);
        Task<Ogretmen> CreateAsync(Ogretmen ogretmen);
        Task<bool> UpdateAsync(Ogretmen ogretmen);
        Task<bool> DeleteAsync(int id);
    }
}