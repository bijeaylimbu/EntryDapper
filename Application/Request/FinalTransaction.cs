namespace TransactionEntry.Application.Request
{
    public class FinalTransaction
    {
        public double DebitTotalAmount { get; set; }
        public double CreditTotalAmount { get; set; }
        public string VoucherId { get; set; }
        
    }
}