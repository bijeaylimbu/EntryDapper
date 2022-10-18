namespace TransactionEntry.Entities
{
    public class TransactionEntry
    {
        public int Id { get; set; }
        public int VoucherId { get; set; }
        public double TotalAmount { get; set; }
    }
}