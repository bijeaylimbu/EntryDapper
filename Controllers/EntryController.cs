using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TransactionEntry.Application.Interface;
using TransactionEntry.Application.Request;

namespace TransactionEntry.Controllers
{
    [Route("api")]
    [ApiController]
    public class EntryController
    {
        private readonly ITransactionEntityRepository _repository;
        public EntryController( ITransactionEntityRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
         [HttpPost("entry")]
        public async Task<int> CreateEntry(EntryRequest request)
        {
            try
            {
                var entry = await _repository.CreateEntry(request);
                return entry  ;

            }
            catch (Exception e)
            {
                throw new Exception("something went wrong");
            }
        }
    }
}