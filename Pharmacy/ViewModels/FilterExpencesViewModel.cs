using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.ViewModels
{
    public class FilterExpencesViewModel
    {
        public FilterExpencesViewModel(int? medicamentId, string dateOfSale, int? counts, double? sellingPrice)
        {
            SelectedMedicamentId = medicamentId;
            SelectedDateOfSale = dateOfSale;
            SelectedCount = counts;
            SelectedSellingPrice = sellingPrice;
        }
        public int? SelectedMedicamentId { get; set; }
        public string SelectedDateOfSale { get; set; }
        public int? SelectedCount { get; set; }
        public double? SelectedSellingPrice { get; set; }
    }
}
