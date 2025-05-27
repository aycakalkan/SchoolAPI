using SchoolApi.Models;

namespace SchoolApi.Services.Interfaces
{
    public interface IDersService
    {
        Task<IEnumerable<Ders>> GetAllAsync();
        Task<Ders?> GetByIdAsync(int id);
        Task<Ders> CreateAsync(Ders ders);
        Task<bool> UpdateAsync(Ders ders);
        Task<bool> DeleteAsync(int id);
    }
}