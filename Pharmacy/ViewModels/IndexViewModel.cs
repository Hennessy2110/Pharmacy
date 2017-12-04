using Pharmacy.ViewModels;
using System.Collections.Generic;

namespace Pharmacy
{
    public class IndexViewModel
    {
        public IEnumerable<Delivers>  Deliver{ get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterDeliversViewModel FilterViewModel { get; set; }
        public SortDeliversViewModel SortViewModel { get; set; }
    }
}
