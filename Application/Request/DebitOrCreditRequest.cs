using System;
using TransactionEntry.Entities;

namespace TransactionEntry.Application.Request
{
    public class DebitOrCreditRequest
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public double Amount { get; set; }
        public DateTime EntryDate { get; set; }
        
    }
}