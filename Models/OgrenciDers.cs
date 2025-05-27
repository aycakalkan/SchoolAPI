namespace SchoolApi.Models
{
    public class OgrenciDers
    {
        public int OgrenciId { get; set; }
        public Ogrenci Ogrenci { get; set; } = null!;

        public int DersId { get; set; }
        public Ders Ders { get; set; } = null!;
    }
}