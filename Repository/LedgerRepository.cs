using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using TransactionEntry.Application.Interface;
using TransactionEntry.Application.Response;
using TransactionEntry.Entities;
using TransactionEntry.Infrastructure.Persistance;

namespace TransactionEntry.Repository
{
    public class LedgerRepository : ILedgerRepository
    {
        private readonly TransactionEntryDBContext _context;

        public LedgerRepository(TransactionEntryDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IReadOnlyList<LedgerResponse>> GetAllLedger()
        {
            var query = "SELECT NAME FROM tbl_ledger";
            using (var connection = _context.Connection())
            {
                var ledger = await connection.QueryAsync<LedgerResponse>(query);
                return ledger.ToList();
            }
        }

        public async Task<IReadOnlyList<LedgerResponse>> FindByName(string name)
        {
            var query = "SELECT NAME FROM tbl_ledger WHERE NAME LIKE @NAME || '%' AND is_cash=true";
            using (var connection = _context.Connection())
            {
                var ledger = await connection.QueryAsync<LedgerResponse>(query, new {NAME=name});
                return ledger.ToList();
            }
        }
    }
}