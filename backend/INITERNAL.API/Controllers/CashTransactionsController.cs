using Aplication.Contracts;
using Aplication.Responses;
using Domain.Entities.Entitie.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Aplication.DTOS.Service.DTO.ServiceDto;

namespace INITERNAL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashTransactionsController : ControllerBase
    {
        private readonly ICashTransactionRepository _cashTransactionRepository;

        public CashTransactionsController(ICashTransactionRepository cashTransactionRepository)
        {
            _cashTransactionRepository = cashTransactionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CashTransactionDto>>> GetCashTransactions()
        {
            var cashTransactions = await _cashTransactionRepository.GetAllCashTransactionsAsync();
               
            var cashTransactionsDto =  cashTransactions.Select(ct => new CashTransactionDto
            {
                TransactionID = ct.Id,
                CashServiceID = ct.CashServiceID,
                Amount = ct.Amount,
                TransactionDate = ct.TransactionDate,
                Status = ct.Status,
                EmployeeID = ct.EmployeeID
            }).ToList();

            return Ok(cashTransactionsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CashTransactionDto>> GetCashTransaction(int id)
        {
            var cashTransaction = await _cashTransactionRepository.GetCashTransactionByIdAsync(id);

            if (cashTransaction is null) return BadRequest(new GeneralReponse(false, "Not Found"));

            var cashTransactionDto = new CashTransactionDto
            {
                TransactionID = cashTransaction.Id,
                CashServiceID = cashTransaction.CashServiceID,                Amount = cashTransaction.Amount,
                TransactionDate = cashTransaction.TransactionDate,
                Status = cashTransaction.Status,
                EmployeeID = cashTransaction.EmployeeID
            };

            return Ok(cashTransactionDto);
        }

        [HttpPost]
        public async Task<ActionResult> PostCashTransaction(CashTransaction cashtransaction)
        {
            var resul = await _cashTransactionRepository.AddCashTransactionAsync(cashtransaction);
            return Ok(resul);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCashTransaction( CashTransaction cashTransaction)
        {
            

             var resul =    await _cashTransactionRepository.UpdateCashTransactionAsync(cashTransaction);

            return Ok(resul);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCashTransaction(int id)
        {
            var resul =  await _cashTransactionRepository.DeleteCashTransactionAsync(id);
            return Ok(resul);
        }
    }
}
