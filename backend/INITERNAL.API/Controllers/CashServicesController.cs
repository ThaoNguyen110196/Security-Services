using System.Transactions;
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
    public class CashServicesController : ControllerBase
    {
        private readonly ICashServiceRepository _cashServiceRepository;
        private readonly ICashTransactionRepository _cashTransactionRepository;
        private readonly ILogServiceEntity _logService;
        public CashServicesController(ICashServiceRepository cashServiceRepository,
              ICashTransactionRepository cashTransactionRepository,
               ILogServiceEntity logService)
        {
            _cashServiceRepository = cashServiceRepository;
           _cashTransactionRepository = cashTransactionRepository;
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CashServiceDto>>> GetCashServices()
        {
            var cashServices = await _cashServiceRepository.GetAllCashServicesAsync();
            var cashServicesDto = cashServices.Select(cs => new CashServiceDto
            {
                CashServiceID = cs.Id,
                ServiceType = cs.ServiceType,
                Scope = cs.Scope!,
                Price = cs.Price!,
                Conditions = cs.Conditions,
                Name = cs.Name
            }).ToList();

            return Ok(cashServicesDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CashServiceDto>> GetCashService(int id)
        {
            var cashService = await _cashServiceRepository.GetCashServiceByIdAsync(id);

            if (cashService is null) return BadRequest(new GeneralReponse(false, "Not Found"));

            var cashServiceDto = new CashServiceDto
            {
                CashServiceID = cashService.Id,
                ServiceType = cashService.ServiceType!,
                Scope = cashService.Scope,
                Price = cashService.Price!,
                Conditions = cashService.Conditions
            };

            return Ok(cashServiceDto);
        }

        [HttpPost]
        public async Task<ActionResult> PostCashService(CashService cashService)
        {
            

               var resul  = await _cashServiceRepository.AddCashServiceAsync(cashService);

            return Ok(resul);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCashService( CashService cashService)
        {
            
            var resul =    await _cashServiceRepository.UpdateCashServiceAsync(cashService);

            return Ok(resul);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCashService(int id)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Lấy thông tin CashService và các CashTransactions liên quan
                var cashService = await _cashServiceRepository.GetCashServiceByIdAsync(id);
                if (cashService == null)
                {
                    return NotFound(new GeneralReponse(false,"cashservice not found"));
                }

                var cashTransactions = await _cashTransactionRepository.GetByCashServiceIdAsync(id);


                // Ghi log thông tin CashService
                var cashServiceDetails = $"CashService details: ID: {cashService.Id}, Name: {cashService.Name}, Price: {cashService.Price}";
                await _logService.LogDeletionAsync(cashService, cashServiceDetails, DateTime.UtcNow);

                // Kiểm tra null và ghi log, xóa các CashTransactions liên quan
                if (cashTransactions != null && cashTransactions.Any())
                {
                    foreach (var transaction in cashTransactions)
                    {
                        var transactionDetails = $"CashTransaction details: ID: {transaction.Id}, " +
                                                 $"Amount: {transaction.Amount}, " +
                                                 $"CashServiceID: {transaction.CashServiceID}, " +
                                                 $"TransactionDate: {transaction.TransactionDate}";

                        await _logService.LogDeletionAsync(transaction, transactionDetails, DateTime.UtcNow);
                        await _cashTransactionRepository.DeleteCashTransactionAsync(transaction.Id);
                    }
                }

                // Xóa CashService
                await _cashServiceRepository.DeleteCashServiceAsync(cashService.Id);

                scope.Complete();
            }

            return Ok(new GeneralReponse(true,"sucessfully delete"));
        }
    }
}
