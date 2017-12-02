using Pharmacy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.ViewModels
{
    public class SortDeliversViewModel
    {
        public DeliversSortState SurnameAscSort { get; private set; }
        public DeliversSortState NameAscSort { get; private set; }
        public DeliversSortState PatronymicAscSort { get; private set; }
        public DeliversSortState ContactPhoneAscSort { get; private set; }
        public DeliversSortState Current { get; private set; }

        public SortDeliversViewModel(DeliversSortState sortOrder)
        {
            SurnameAscSort = sortOrder == DeliversSortState.SurnameAsc ? DeliversSortState.SurnameIdDesc : DeliversSortState.SurnameAsc;
            NameAscSort = sortOrder == DeliversSortState.NameAsc ? DeliversSortState.NameDesc : DeliversSortState.NameAsc;
            PatronymicAscSort = sortOrder == DeliversSortState.PatronymicAsc ? DeliversSortState.PatronymicDesc : DeliversSortState.PatronymicAsc;
            ContactPhoneAscSort = sortOrder == DeliversSortState.ContactPhoneAsc ? DeliversSortState.ContactPhoneDesc : DeliversSortState.ContactPhoneAsc;
            Current = sortOrder;
        }
    }
}
