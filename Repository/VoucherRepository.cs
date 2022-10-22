using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TransactionEntry.Application.Interface;
using TransactionEntry.Application.Response;
using TransactionEntry.Infrastructure.Persistance;

namespace TransactionEntry.Repository
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly TransactionEntryDBContext _context;
        private IVoucherRepository _voucherRepositoryImplementation;

        public VoucherRepository(TransactionEntryDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<VoucherResponse> GetVoucherId(int id)
        {
            var query = "SELECT NAME FROM tbl_voucher WHERE VOUCHER_ID = @Id";
            using (var connection = _context.Connection())
            {
                return await  connection.QueryFirstAsync<VoucherResponse>(query, new {Id=id});
            }
        }

        public async Task<IReadOnlyList<VoucherResponse>> GetAllVoucher()
        {
             var query = "SELECT VOUCHER_ID, NAME FROM tbl_voucher";
            using (var connection = _context.Connection())
            {
                var ledger = await connection.QueryAsync<VoucherResponse>(query);
                return ledger.ToList();
            }
        }

        public async Task<IReadOnlyList<LedgerResponse>> GetLedgerByVoucher(int id)
        {
            if ( id == 1)
            {
                var query = "SELECT * FROM tbl_voucher tv FULL JOIN tbl_ledger tl ON tv.voucher_id=tl.voucher_id  WHERE tl.voucher_id= @id AND is_cash=true";
                using (var connection = _context.Connection())
                {
                    var ledger = await connection.QueryAsync<LedgerResponse>(query, new {id});
                    return ledger.ToList();
                }   
            }
            else
            {
                var query = "SELECT * FROM tbl_voucher tb JOIN tbl_ledger tl ON tb.voucher_id=tl.voucher_id ";
                using (var connection = _context.Connection())
                {
                    var ledger = await connection.QueryAsync<LedgerResponse>(query);
                    return ledger.ToList();
                }   
            }
        }
    }
}