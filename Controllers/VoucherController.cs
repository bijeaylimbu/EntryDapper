using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TransactionEntry.Application.Interface;
using TransactionEntry.Application.Response;

namespace TransactionEntry.Controllers
{
    [Route("api")]
    [ApiController]
    public class VoucherController
    {
        private readonly IVoucherRepository _repository;

        public VoucherController(IVoucherRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<VoucherResponse> GetByName(int id)
        {
            var voucher = await _repository.GetVoucherId(id);
            return voucher;
        }

        [HttpGet("get-all-voucher")]
        public async Task<IReadOnlyList<VoucherResponse>> GetAllVoucher()
        {
            var voucher = await _repository.GetAllVoucher();
            return voucher;
        }
       [HttpGet("get-ledger-by-voucher/{id}")]
        public async Task<IReadOnlyList<LedgerResponse>> GetLedgerByVoucher(int id)
        {
            var voucher = await _repository.GetLedgerByVoucher(id);
            return voucher;
        }
    }
}