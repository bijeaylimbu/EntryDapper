using System.Threading.Tasks;
using TransactionEntry.Application.Request;

namespace TransactionEntry.Application.Interface
{
    public interface IDebitOrCreditRepository
    {
        Task<string> AddDebitOrCreditAmount(AmountRequest request);
        
        Task<string> AddFinalTransaction(FinalTransaction request);
        Task<int> UpdateEntry(DebitOrCreditRequest request,  int id);
    };
}