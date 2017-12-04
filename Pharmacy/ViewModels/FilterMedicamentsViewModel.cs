using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.ViewModels
{
    public class FilterMedicamentsViewModel
    {
        public FilterMedicamentsViewModel(string name, string annotation, string producer, string units, string storage)
        {
            SelectedName = name;
            SelectedAnnotation = annotation;
            SelectedProducer = producer;
            SelectedUnits = units;
            SelectedStorage = storage;
        }
        public string SelectedName { get; set; }
        public string SelectedAnnotation { get; set; }
        public string SelectedProducer { get; set; }
        public string SelectedUnits { get; set; }
        public string SelectedStorage { get; set; }
    }
}
