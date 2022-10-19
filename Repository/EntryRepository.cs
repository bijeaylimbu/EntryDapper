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
            try
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
                    int result = await connection.ExecuteScalarAsync<int>(query, parameters);
                    return result;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<int> EnterDebitOrCredit(DebitOrCreditRequest request)
        {
            try
            {
                var query =
                    "INSERT INTO tbl_entry(TYPE, SUB_TYPE, AMOUNT, ENTRY_DATE) VALUES (@TYPE,@SUB_TYPE, @AMOUNT, @ENTRY_DATE) RETURNING entry_id";
                var parameters = new DynamicParameters();
                parameters.Add("LEDGER", request.Type);
                parameters.Add("DEBIT", request.SubType);
                parameters.Add("CREDIT", request.Amount);
                parameters.Add("CREATED_DATE", request.EntryDate);
                using (var connection = _context.Connection())
                {
                    int result = await connection.ExecuteScalarAsync<int>(query, parameters);
                    
                    return result;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}