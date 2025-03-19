using Aplication.Contracts;
using Domain.Entities.Entitie.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INITERNAL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerSupportController : ControllerBase
    {
        private readonly IEmployeeSupportRepository _employeeSupportRepository;

        public CustomerSupportController(IEmployeeSupportRepository employeeSupportRepository)
        {
            _employeeSupportRepository = employeeSupportRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeSupport>>> GetAllCustomerSupports()
        {
            var customerSupports = await _employeeSupportRepository.GetAllAsync();
            return Ok(customerSupports);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeSupport>> GetCustomerSupportById(int id)
        {
            var customerSupport = await _employeeSupportRepository.GetByIdAsync(id);
            if (customerSupport == null)
            {
                return NotFound(new { success = false, message = "Not Found" });
            }
            return Ok(customerSupport);
        }

        [HttpPost]
        public async Task<ActionResult> AddCustomerSupport(EmployeeSupport customerSupport)
        {
            
             var resul =   await _employeeSupportRepository.AddAsync(customerSupport);
            return Ok(resul);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerSupport (EmployeeSupport customerSupport)
        {
           
            

             var resul =   await _employeeSupportRepository.UpdateAsync(customerSupport);
            return Ok(resul);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerSupport(int id)
        {
            

              var resul =  await _employeeSupportRepository.DeleteAsync(id);
            return Ok(resul);
        }
    }
}
