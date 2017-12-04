using Pharmacy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.ViewModels
{
    public class SortExpencesViewModel
    {
        public ExpencesSortState MedicamentIdAscSort { get; private set; }
        public ExpencesSortState DateOfSaleAscSort { get; private set; }
        public ExpencesSortState CountAscSort { get; private set; }
        public ExpencesSortState SellingPriceAscSort { get; private set; }
        public ExpencesSortState Current { get; private set; }

        public SortExpencesViewModel(ExpencesSortState sortOrder)
        {
            MedicamentIdAscSort = sortOrder == ExpencesSortState.MedicamentIdAsc ? ExpencesSortState.MedicamentIdDesc : ExpencesSortState.MedicamentIdAsc;
            DateOfSaleAscSort = sortOrder == ExpencesSortState.DateOfSaleAsc ? ExpencesSortState.DateOfSaleDesc : ExpencesSortState.DateOfSaleAsc;
            CountAscSort = sortOrder == ExpencesSortState.CountAsc ? ExpencesSortState.CountDesc : ExpencesSortState.CountAsc;
            SellingPriceAscSort = sortOrder == ExpencesSortState.SellingPriceAsc ? ExpencesSortState.SellingPriceDesc : ExpencesSortState.SellingPriceAsc;
            Current = sortOrder;
        }
    }
}
