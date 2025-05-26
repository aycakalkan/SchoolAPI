using System.Collections.Generic;

namespace SchoolApi.Models
{
    public class Ogretmen
    {
        public int Id { get; set; }
        public string Ad { get; set; }

        public ICollection<Ders> Dersler { get; set; }
    }
}

