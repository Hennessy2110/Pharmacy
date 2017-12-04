using System.Collections.Generic;

namespace Pharmacy.ViewModels
{
    public class ExpencesViewModel
    {
        public IEnumerable<Expence> Expences { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterExpencesViewModel FilterViewModel { get; set; }
        public SortExpencesViewModel SortViewModel { get; set; }
    }
}
