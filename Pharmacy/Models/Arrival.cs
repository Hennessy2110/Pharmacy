namespace Pharmacy
{
    public partial class Arrival
    {
        public int ArrivalId { get; set; }
        public int MedicamentId { get; set; }
        public string ReceiptDate { get; set; }
        public int Count { get; set; }
        public int DeliverId { get; set; }
        public double PurchasePrice { get; set; }

        public Delivers Deliver { get; set; }
        public Medicaments Medicament { get; set; }
    }
}
