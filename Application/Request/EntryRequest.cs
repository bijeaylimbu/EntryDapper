using System;

namespace TransactionEntry.Application.Request
{
    public class EntryRequest
    {
        public string Ledger { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public  DateTime  CreatedDate { get; set; }
    }
}
