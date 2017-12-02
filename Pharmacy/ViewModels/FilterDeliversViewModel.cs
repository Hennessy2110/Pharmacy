using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.ViewModels
{
    public class FilterDeliversViewModel
    {
        public FilterDeliversViewModel(string surname, string name, string patronymic, int? contactPhone)
        {
            SelectedSurname = surname;
            SelectedName = name;
            SelectedPatronymic = patronymic;
            SelectedContactPhone = contactPhone;
        }
        public string SelectedSurname { get; set; }
        public string SelectedName { get; set; }
        public string SelectedPatronymic { get; set; }
        public int? SelectedContactPhone { get; set; }
    }
}
