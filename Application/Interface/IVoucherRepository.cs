using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionEntry.Application.Response;

namespace TransactionEntry.Application.Interface
{
    public interface IVoucherRepository
    {
        Task<VoucherResponse> GetVoucherId(int id);

        Task<IReadOnlyList<VoucherResponse>> GetAllVoucher();

        Task<IReadOnlyList<LedgerResponse>> GetLedgerByVoucher(int id);
    }
}