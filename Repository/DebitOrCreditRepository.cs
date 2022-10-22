using System;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
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
                if (request.EntryId == null)
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
                            return masterEntry;
                        }

                        else if (result != null && request.Type.ToUpper()=="DEBIT")
                        {
                            var masterQuery =
                                "INSERT INTO tbl_entry(LEDGER, DEBIT, CREDIT, ENTRY_ID) VALUES (@LEDGER,@DEBIT, @CREDIT, @ENTRY_ID) RETURNING entry_id";
                            var masterParameters = new DynamicParameters();
                            masterParameters.Add("LEDGER", request.SubType);
                            masterParameters.Add("DEBIT", request.Amount);
                            masterParameters.Add("CREDIT", null);
                            masterParameters.Add("ENTRY_ID", Int32.Parse(result));
                            string masterEntry = await connection.ExecuteScalarAsync<string>(masterQuery, masterParameters);
                            return masterEntry;
                        }
                        throw new ArgumentNullException(nameof(result));
                    }     
                }
                using (var connection = _context.Connection())
                {   int result=Int32.Parse(request.EntryId);
                    double total = 0.00;
                    if (request.Type.ToUpper()=="CREDIT")
                    {
                        var masterQuery =
                            "INSERT INTO tbl_entry(LEDGER, DEBIT, CREDIT, ENTRY_ID) VALUES (@LEDGER,@DEBIT, @CREDIT, @ENTRY_ID) RETURNING entry_id";
                        var masterParameters = new DynamicParameters();
                        masterParameters.Add("LEDGER", request.SubType);
                        masterParameters.Add("DEBIT", null);
                        masterParameters.Add("CREDIT", request.Amount);
                        masterParameters.Add("ENTRY_ID", result );
                        string masterEntry = await connection.ExecuteScalarAsync<string>(masterQuery, masterParameters);
                        return masterEntry;
                    }

                    else if (result != null && request.Type.ToUpper()=="DEBIT")
                    {
                        var masterQuery =
                            "INSERT INTO tbl_entry(LEDGER, DEBIT, CREDIT, ENTRY_ID) VALUES (@LEDGER,@DEBIT, @CREDIT, @ENTRY_ID) RETURNING entry_id";
                        var masterParameters = new DynamicParameters();
                        masterParameters.Add("LEDGER", request.SubType);
                        masterParameters.Add("DEBIT", request.Amount);
                        masterParameters.Add("CREDIT", null);
                        masterParameters.Add("ENTRY_ID", result);
                        string masterEntry = await connection.ExecuteScalarAsync<string>(masterQuery, masterParameters);
                        return masterEntry;
                    }
                    throw new ArgumentNullException(nameof(result));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
           
        }

        public async Task<string> AddFinalTransaction(FinalTransaction request)
        {
            if (request.CreditTotalAmount.Equals( request.DebitTotalAmount))
            {
                var tranQuery =
                    "INSERT INTO tbl_transaction_entry(VOUCHER_ID, TOTAL) VALUES (@VOUCHER_ID,@TOTAL) RETURNING voucher_id";
                var tranParameters = new DynamicParameters();
                tranParameters.Add("VOUCHER_ID", Int32.Parse(request.VoucherId));
                tranParameters.Add("TOTAL", request.CreditTotalAmount+ request.DebitTotalAmount);
                using var connection = _context.Connection();
                var tranEntry = await connection.ExecuteScalarAsync<string>(tranQuery, tranParameters);
                return tranEntry;
            }

            return "debit and credit amount doesn't match";
        }
    }
}