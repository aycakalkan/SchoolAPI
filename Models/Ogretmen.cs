using System.ComponentModel.DataAnnotations;

namespace SchoolApi.Models
{
    public class Ogretmen
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Öğretmen adı zorunludur")]
        [StringLength(100, ErrorMessage = "Ad en fazla 100 karakter olabilir")]
        public string Ad { get; set; } = string.Empty;

        public ICollection<Ders> Dersler { get; set; } = new List<Ders>();
    }
}