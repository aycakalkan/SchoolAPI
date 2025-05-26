using System.Collections.Generic;

namespace SchoolApi.Models
{
    public class Ogrenci
    {
        public int Id { get; set; }
        public string Ad { get; set; }

        public ICollection<OgrenciDers> OgrenciDersler { get; set; }
    }
}


