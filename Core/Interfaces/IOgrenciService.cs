using SchoolApi.Models;

namespace SchoolApi.Services.Interfaces
{
    public interface IOgrenciService
    {
        Task<IEnumerable<Ogrenci>> GetAllAsync();
        Task<Ogrenci?> GetByIdAsync(int id);
        Task<Ogrenci> CreateAsync(Ogrenci ogrenci);
        Task<bool> UpdateAsync(Ogrenci ogrenci);
        Task<bool> DeleteAsync(int id);
    }
}