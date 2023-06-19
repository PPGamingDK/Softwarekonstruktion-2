using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekssammen
{
   public class Containere
    {
        public int Id { get; set; }
        public int Weight { get; set; }
        public string Airport { get; set; }

        public Containere(int weight, string airport) // Når vi opretter containere
        {
            Id = 0;
            Weight = weight;
            Airport = airport;
        }

        public Containere(int id, int weight, string airport) // Overloading, når vi henter fra db
        {
            Id = id;
            Weight = weight;
            Airport = airport;
        }

    }
}
