using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionEntry.Application.Response;
using TransactionEntry.Entities;

namespace TransactionEntry.Application.Interface
{
    public interface ILedgerRepository
    {
        Task<IReadOnlyList<LedgerResponse>> GetAllLedger();

        Task<IReadOnlyList<LedgerResponse>> FindByName(string name);
    }
}