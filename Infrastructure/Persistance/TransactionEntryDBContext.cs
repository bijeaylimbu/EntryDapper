using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace TransactionEntry.Infrastructure.Persistance
{
    public class TransactionEntryDBContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public TransactionEntryDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection") ??
                            throw new    ArgumentNullException(_connectionString);
        }

        public IDbConnection Connection() =>
            new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
    }
}