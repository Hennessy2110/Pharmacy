using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.ViewModels
{
    public class FilterArrivalsViewModel
    {
        public FilterArrivalsViewModel(int? medicamentId, string receiptDate, string receiptDateFrom, string receiptDateTo, int? counts, int? deliverId, double? purchasePrice, string producer)
        {
            SelectedMedicamentId = medicamentId;
            SelectedReceiptDate = receiptDate;
            SelectedReceiptDateFrom = receiptDateFrom;
            SelectedReceiptDateTo = receiptDateTo;
            SelectedCount = counts;
            SelectedDeliverId = deliverId;
            SelectedPurchasePrice = purchasePrice;
            SelectedProducer = producer;
        }
        public int? SelectedMedicamentId { get; set; }
        public string SelectedReceiptDate { get; set; }
        public string SelectedReceiptDateFrom { get; set; }
        public string SelectedReceiptDateTo { get; set; }
        public int? SelectedCount { get; set; }
        public int? SelectedDeliverId { get; set; }
        public double? SelectedPurchasePrice { get; set; }
        public string SelectedProducer { get; set; }
    }
}
