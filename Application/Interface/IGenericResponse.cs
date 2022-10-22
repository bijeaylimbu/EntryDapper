using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionEntry.Application.Request;
using TransactionEntry.Controllers;

namespace TransactionEntry.Application.Interface
{
    public interface IGenericResponse
    {
         Task<int> CreateEntry(EntryRequest request);

         Task<int> EnterDebitOrCredit(DebitOrCreditRequest request);
         
         Task<IReadOnlyList<EntryResponse>> getAllEntryByEntryId(int entryId);

         Task<TotalAmountResponse> GetEntryTotalById(string id);
    }
}