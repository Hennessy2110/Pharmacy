using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.ViewModels
{
    public class ArrivalsViewModel
    {
        public IEnumerable<Arrival> Arrival { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterArrivalsViewModel FilterViewModel { get; set; }
        public SortArrivalsViewModel SortViewModel { get; set; }
    }
}
