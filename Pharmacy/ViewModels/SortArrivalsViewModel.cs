using Pharmacy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.ViewModels
{
    public class SortArrivalsViewModel
    {
        public ArrivalsSortState MedicamentIdAscSort { get; private set; }
        public ArrivalsSortState ReceiptDateAscSort { get; private set; }
        public ArrivalsSortState CountAscSort { get; private set; }
        public ArrivalsSortState DeliverIdAscSort { get; private set; }
        public ArrivalsSortState PurchasePriceAscSort { get; private set; }
        public ArrivalsSortState Current { get; private set; }

        public SortArrivalsViewModel(ArrivalsSortState sortOrder)
        {
            MedicamentIdAscSort = sortOrder == ArrivalsSortState.MedicamentIdAsc ? ArrivalsSortState.MedicamentIdDesc : ArrivalsSortState.MedicamentIdAsc;
            ReceiptDateAscSort = sortOrder == ArrivalsSortState.ReceiptDateAsc ? ArrivalsSortState.ReceiptDateDesc : ArrivalsSortState.ReceiptDateAsc;
            CountAscSort = sortOrder == ArrivalsSortState.CountAsc ? ArrivalsSortState.CountDesc : ArrivalsSortState.CountAsc;
            DeliverIdAscSort = sortOrder == ArrivalsSortState.DeliverIdAsc ? ArrivalsSortState.DeliverIdDesc : ArrivalsSortState.DeliverIdAsc;
            PurchasePriceAscSort = sortOrder == ArrivalsSortState.PurchasePriceAsc ? ArrivalsSortState.PurchasePriceDesc : ArrivalsSortState.PurchasePriceAsc;
            Current = sortOrder;
        }
    }
}
