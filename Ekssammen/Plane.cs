using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekssammen
{
   public class Plane
    {
        public int Id { get; set; }
        public int MaxWeight { get; set; }
        public string Registration { get; set; }

        public Plane(int maxweight, string registration) // Oprettelse af fly.
        {
            Id = 0;
            MaxWeight = maxweight;
            Registration = registration;
        }

        public Plane(int id, int maxweight, string registration) // Overloading, når vi henter fra DB.
        {
            Id = id;
            MaxWeight = maxweight;
            Registration = registration;
        }
    }
}
