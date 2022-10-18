using System;
using System.Threading.Tasks;
using Dapper;
using TransactionEntry.Application.Interface;
using TransactionEntry.Application.Request;
using TransactionEntry.Infrastructure.Persistance;

namespace TransactionEntry.Repository
{
    public class EntryRepository: ITransactionEntityRepository
    {
        private readonly TransactionEntryDBContext _context;

        public EntryRepository(TransactionEntryDBContext context)
        {
            _context = context ?? throw  new ArgumentNullException(nameof(context));
        }
        public  async Task<int> CreateEntry(EntryRequest request)
        {
            var query =
                "INSERT INTO tbl_entry(LEDGER, DEBIT, CREDIT, CREATED_DATE) VALUES (@LEDGER,@DEBIT, @CREDIT, @CREATED_DATE) RETURNING id";
            var parameters = new DynamicParameters();
            parameters.Add("LEDGER", request.Ledger);
            parameters.Add("DEBIT", request.Debit);
            parameters.Add("CREDIT", request.Credit);
            parameters.Add("CREATED_DATE", request.CreatedDate);
            using (var connection = _context.Connection())
            {
             int result=  await connection.ExecuteScalarAsync<int>(query, parameters);
             return result;
            }
        }
    }
}