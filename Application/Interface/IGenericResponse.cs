using System.Threading.Tasks;
using TransactionEntry.Application.Request;

namespace TransactionEntry.Application.Interface
{
    public interface IGenericResponse
    {
         Task<int> CreateEntry(EntryRequest request);
    }
}