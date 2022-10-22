using System;

namespace TransactionEntry.Application.Request
{
    public class EntryResponse
    { 
        public int Id { get; set; }
        public string Ledger { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public  DateTime  CreatedDate { get; set; }
        
    }
}
