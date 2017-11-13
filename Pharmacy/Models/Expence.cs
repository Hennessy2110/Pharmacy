using System;
using System.Collections.Generic;

namespace Pharmacy
{
    public partial class Expence
    {
        public int ExpenseId { get; set; }
        public int MedicamentId { get; set; }
        public string DateOfSale { get; set; }
        public int Count { get; set; }
        public double SellingPrice { get; set; }

        public Medicaments Medicament { get; set; }
    }
}
