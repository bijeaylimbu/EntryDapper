using System;
using System.Threading.Tasks;
using Dapper;
using TransactionEntry.Application.Interface;
using TransactionEntry.Application.Request;
using TransactionEntry.Infrastructure.Persistance;

namespace TransactionEntry.Repository
{
    public class DebitOrCreditRepository : IDebitOrCreditRepository
    {
        private readonly TransactionEntryDBContext _context;

        public DebitOrCreditRepository(TransactionEntryDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<string> AddDebitOrCreditAmount(AmountRequest request)
        {
            try
            {
                var query =
                    "INSERT INTO tbl_entry_debit_or_credit(TYPE, SUB_TYPE, AMOUNT, ENTRY_DATE) VALUES (@TYPE,@SUB_TYPE, @AMOUNT, @ENTRY_DATE) RETURNING  entry_id";
                var parameters = new DynamicParameters();
                parameters.Add("TYPE", request.Type);
                parameters.Add("SUB_TYPE", request.SubType);
                parameters.Add("AMOUNT", request.Amount);
                parameters.Add("ENTRY_DATE", request.EntryDate);
                using (var connection = _context.Connection())
                {
                    string result = await connection.ExecuteScalarAsync<string>(query, parameters);
                    double total = 0.00;
                    if (result != null && request.Type.ToUpper()=="CREDIT")
                    {
                        var masterQuery =
                            "INSERT INTO tbl_entry(LEDGER, DEBIT, CREDIT, ENTRY_ID) VALUES (@LEDGER,@DEBIT, @CREDIT, @ENTRY_ID) RETURNING entry_id";
                        var masterParameters = new DynamicParameters();
                        masterParameters.Add("LEDGER", request.SubType);
                        masterParameters.Add("DEBIT", null);
                        masterParameters.Add("CREDIT", request.Amount);
                        masterParameters.Add("ENTRY_ID", Int32.Parse(result) );
                        string masterEntry = await connection.ExecuteScalarAsync<string>(masterQuery, masterParameters);
                        if (masterEntry != null)
                        {
                            total += request.Amount;
                            int voucher_id = Int32.Parse(result);
                            string findQuery = "SELECT * FROM tbl_transaction_entry WHERE voucher_id= @Voucher_Id";
                            string tranData = await connection.QueryFirstOrDefaultAsync<string>(findQuery, new {voucher_id  });
                            if (tranData != null)
                            {
                                var tranQuery =
                                    "INSERT INTO tbl_transaction_entry(VOUCHER_ID, TOTAL) VALUES (@VOUCHER_ID,@TOTAL) RETURNING voucher_id";
                                var tranParameters = new DynamicParameters();
                                tranParameters.Add("VOUCHER_ID", Int32.Parse(result));
                                tranParameters.Add("TOTAL", total);
                                string tranEntry = await connection.ExecuteScalarAsync<string>(tranQuery, tranParameters);
                                return voucher_id.ToString();
                            }
                            else
                            {
                                var updateQuery =
                                    "UPDATE tbl_transaction_entry SET TOTAL= @TOTAL WHERE voucher_id= @voucher_id ";
                                var updateParameter = new DynamicParameters();
                                updateParameter.Add("TOTAL", total);
                                 await connection.ExecuteAsync(updateQuery, updateParameter);
                                 return result;
                            }
                        }
                    }

                    else if (result != null && request.Type.ToUpper()=="DEBIT")
                    {
                        var masterQuery =
                            "INSERT INTO tbl_entry(LEDGER, DEBIT, CREDIT, ENTRY_ID) VALUES (@LEDGER,@DEBIT, @CREDIT, @ENTRY_ID) RETURNING entry_id";
                        var masterParameters = new DynamicParameters();
                        masterParameters.Add("LEDGER", request.SubType);
                        masterParameters.Add("DEBIT", request.Amount);
                        masterParameters.Add("CREDIT", null);
                        masterParameters.Add("ENTRY_ID", Int32.Parse(result) );
                        string masterEntry = await connection.ExecuteScalarAsync<string>(masterQuery, masterParameters);
                        if (masterEntry != null)
                        {
                            total += request.Amount;
                            int voucher_id = Int32.Parse(result);
                            string findQuery = "SELECT id FROM tbl_transaction_entry WHERE voucher_id= @VOUCHER_ID";
                            string tranData = await connection.QueryFirstOrDefaultAsync<string>(findQuery, new {voucher_id  });
                            if (tranData == null)
                            {
                                var tranQuery =
                                    "INSERT INTO tbl_transaction_entry(VOUCHER_ID, TOTAL) VALUES (@VOUCHER_ID,@TOTAL) RETURNING entry_id";
                                var tranParameters = new DynamicParameters();
                                tranParameters.Add("VOUCHER_ID", Int32.Parse(result));
                                tranParameters.Add("TOTAL", total);
                                string tranEntry = await connection.ExecuteScalarAsync<string>(masterQuery, masterParameters);
                            }
                            else
                            {
                                var updateQuery =
                                    "UPDATE tbl_transaction_entry SET TOTAL= @TOTAL WHERE voucher_id= @voucher_id ";
                                var updateParameter = new DynamicParameters();
                                updateParameter.Add("TOTAL", total);
                                await connection.ExecuteAsync(updateQuery, updateParameter);
                                return result;
                            }
                        }
                    }
                    throw new ArgumentNullException(nameof(result));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
           
        }
    }
}