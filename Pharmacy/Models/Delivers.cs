using System.Collections.Generic;

namespace Pharmacy
{
    public partial class Delivers
    {
        public Delivers()
        {
            Arrival = new HashSet<Arrival>();
        }

        public int DeliverId { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public int ContactPhone { get; set; }

        public ICollection<Arrival> Arrival { get; set; }
    }
}
