using System;

namespace TransactionEntry.Application.Request
{
    public class AmountRequest
    {
        public string Type { get; set; }
        public string SubType { get; set; }
        public double Amount { get; set; }
        public DateTime EntryDate { get; set; }
        public int EntryId { get; set; }
    }
}