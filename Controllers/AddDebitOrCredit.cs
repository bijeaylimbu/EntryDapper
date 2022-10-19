using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TransactionEntry.Application.Interface;
using TransactionEntry.Application.Request;

namespace TransactionEntry.Controllers
{
    [Microsoft.AspNetCore.Components.Route(("api"))]
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
    }
}