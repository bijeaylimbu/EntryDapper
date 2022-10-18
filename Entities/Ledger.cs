namespace TransactionEntry.Entities
{
    public class Ledger
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
    }
}