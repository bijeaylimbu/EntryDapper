using System.Collections.Generic;

namespace TransactionEntry.Application.Response
{
    public class VoucherResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<LedgerResponse> Ledger { get; set; }
    }
}