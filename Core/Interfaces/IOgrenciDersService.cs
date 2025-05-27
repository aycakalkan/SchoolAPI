namespace SchoolApi.Services.Interfaces
{
    public interface IOgrenciDersService
    {
        Task<bool> AddCourseAsync(int ogrenciId, int dersId);
        Task<bool> RemoveCourseAsync(int ogrenciId, int dersId);
    }
}