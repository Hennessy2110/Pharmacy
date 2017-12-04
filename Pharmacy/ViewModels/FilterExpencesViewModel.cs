namespace Pharmacy.ViewModels
{
    public class FilterExpencesViewModel
    {
        public FilterExpencesViewModel(int? medicamentId, string dateOfSale, int? counts, double? sellingPrice, string dateOfSaleFrom, string dateOfSaleTo)
        {
            SelectedMedicamentId = medicamentId;
            SelectedDateOfSale = dateOfSale;
            SelectedCount = counts;
            SelectedSellingPrice = sellingPrice;
            SelectedDateOfSaleFrom = dateOfSaleFrom;
            SelectedDateOfSaleTo = dateOfSaleTo;
        }
        public int? SelectedMedicamentId { get; set; }
        public string SelectedDateOfSale { get; set; }
        public int? SelectedCount { get; set; }
        public double? SelectedSellingPrice { get; set; }
        public string SelectedDateOfSaleFrom { get; set; }
        public string SelectedDateOfSaleTo { get; set; }
    }
}
