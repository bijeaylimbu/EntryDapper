using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TransactionEntry.Application.Interface;
using TransactionEntry.Application.Response;
using TransactionEntry.Entities;

namespace TransactionEntry.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api")]
    [ApiController]
    public class LedgerController
    {

        private readonly ILedgerRepository _repository;

        public LedgerController(ILedgerRepository repository)
        {
            _repository=repository ?? throw new ArgumentNullException(nameof(repository));
        }
        [HttpGet("all-ledger")]
        public async Task<IReadOnlyList<LedgerResponse>> GetAllLedger()
        {
            var ledger = await _repository.GetAllLedger();
            return ledger;
        }
        [HttpGet("get-ledger-by-name/{name}")]
        public async Task<IReadOnlyList<LedgerResponse>> GetLedgerByName(string name)
        {
            var legder=await _repository.FindByName(name);
            return legder;
        }
    }
}