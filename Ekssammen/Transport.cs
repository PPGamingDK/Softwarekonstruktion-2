using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekssammen
{
    public class Transport
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public Plane Plane { get; set; }
        public Containere Containere { get; set; } // Property

        public Transport(string date, Plane plane, Containere containere) // Oprettelse af Transport
        {
            Id = 0;
            Date = date;
            Plane = plane;
            Containere = containere;
        }

        public Transport(int id, string date, Plane plane, Containere containere) // Overloading af oprettelse af property
        {
            Id = id;
            Date = date;
            Plane = plane;
            Containere = containere;
        }
    }
}
