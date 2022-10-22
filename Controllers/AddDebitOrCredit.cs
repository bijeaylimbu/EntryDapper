using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TransactionEntry.Application.Interface;
using TransactionEntry.Application.Request;

namespace TransactionEntry.Controllers
{
    [Route(("api"))]
    [ApiController]
    public class AddDebitOrCredit
    {
        private readonly IDebitOrCreditRepository _repository;

        public AddDebitOrCredit(IDebitOrCreditRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        [HttpPost("add-entry")]
        public async Task<string> AddEntry(AmountRequest request)
        {
           
                var addAmount = await _repository.AddDebitOrCreditAmount(request);
                return  addAmount;
        }

        [HttpPost("add-tran-entry")]
        public async Task<string> AddTransactionEntry(FinalTransaction finalTransaction)
        {
            var entry = await _repository.AddFinalTransaction(finalTransaction);
            return entry;
        }

        [HttpPut("update-entry/{id}")]
        public async Task<int> UpdateEntry(DebitOrCreditRequest request, int id)
        {
            var update = await _repository.UpdateEntry(request, id);
            return update;
        }
    }
}