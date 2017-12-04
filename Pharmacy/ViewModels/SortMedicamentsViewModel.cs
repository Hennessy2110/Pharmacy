using Pharmacy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.ViewModels
{
    public class SortMedicamentsViewModel
    {
        public MedicamentsSortState NameAscSort { get; private set; }
        public MedicamentsSortState AnnotationAscSort { get; private set; }
        public MedicamentsSortState ProducerAscSort { get; private set; }
        public MedicamentsSortState UnitsAscSort { get; private set; }
        public MedicamentsSortState StorageAscSort { get; private set; }
        public MedicamentsSortState Current { get; private set; }

        public SortMedicamentsViewModel(MedicamentsSortState sortOrder)
        {
            NameAscSort = sortOrder == MedicamentsSortState.NameAsc ? MedicamentsSortState.NameDesc : MedicamentsSortState.NameAsc;
            AnnotationAscSort = sortOrder == MedicamentsSortState.AnnotationAsc ? MedicamentsSortState.AnnotationDesc : MedicamentsSortState.AnnotationAsc;
            ProducerAscSort = sortOrder == MedicamentsSortState.ProducerAsc ? MedicamentsSortState.ProducerDesc : MedicamentsSortState.ProducerAsc;
            UnitsAscSort = sortOrder == MedicamentsSortState.UnitsAsc ? MedicamentsSortState.UnitsDesc : MedicamentsSortState.UnitsAsc;
            StorageAscSort = sortOrder == MedicamentsSortState.StorageAsc ? MedicamentsSortState.StorageDesc : MedicamentsSortState.StorageAsc;
            Current = sortOrder;
        }
    }
}
