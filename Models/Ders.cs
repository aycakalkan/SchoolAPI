using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Models
{
    public class Ders
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ders adı zorunludur")]
        [StringLength(100, ErrorMessage = "Ders adı en fazla 100 karakter olabilir")]
        public string DersAdi { get; set; } = string.Empty;

        [Required(ErrorMessage = "Öğretmen ID zorunludur")]
        public int OgretmenId { get; set; }

        public Ogretmen? Ogretmen { get; set; }

        public ICollection<OgrenciDers> OgrenciDersler { get; set; } = new List<OgrenciDers>();
    }
}