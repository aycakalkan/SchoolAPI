using System.Collections.Generic;

namespace SchoolApi.Models
{
    public class Ders
    {
        public int Id { get; set; }
        public string DersAdi { get; set; }

        public int OgretmenId { get; set; }
        public Ogretmen Ogretmen { get; set; }

        public ICollection<OgrenciDers> OgrenciDersler { get; set; }
    }
}


