using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.ViewModels
{
    public class MedicamentsViewModel
    {
        public IEnumerable<Medicaments> Medicaments { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterMedicamentsViewModel FilterViewModel { get; set; }
        public SortMedicamentsViewModel SortViewModel { get; set; }
    }
}
