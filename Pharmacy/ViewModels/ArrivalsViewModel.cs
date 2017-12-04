using System.Collections.Generic;

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
