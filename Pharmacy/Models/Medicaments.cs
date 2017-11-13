using System;
using System.Collections.Generic;

namespace Pharmacy
{
    public partial class Medicaments
    {
        public Medicaments()
        {
            Arrival = new HashSet<Arrival>();
            Expence = new HashSet<Expence>();
        }

        public int MedicamentId { get; set; }
        public string Name { get; set; }
        public string Annotation { get; set; }
        public string Producer { get; set; }
        public string Units { get; set; }
        public string Storage { get; set; }

        public ICollection<Arrival> Arrival { get; set; }
        public ICollection<Expence> Expence { get; set; }
    }
}
