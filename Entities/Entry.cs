using System;

namespace TransactionEntry.Entities
{
    public class Entry
    {
        public int Id { get; set; }
        public string Ledger { get; set; }
        public double Credit { get; set; }
        public double Debit { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}