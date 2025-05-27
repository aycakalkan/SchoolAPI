using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Models
{
    public class Ogrenci
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Öğrenci adı zorunludur")]
        [StringLength(100, ErrorMessage = "Ad en fazla 100 karakter olabilir")]
        public string Ad { get; set; } = string.Empty;

        public ICollection<OgrenciDers> OgrenciDersler { get; set; } = new List<OgrenciDers>();
    }
}