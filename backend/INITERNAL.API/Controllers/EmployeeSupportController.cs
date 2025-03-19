using Aplication.Responses;
using Domain.Entities.Entitie.Employee;
using Infrastruture.Implementtations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Aplication.DTOS.Service.DTO.ServiceDto;

namespace INITERNAL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeSupportController : ControllerBase
    {
        private readonly SupportCustommerRepository _SupportCustommerRepository;

        public EmployeeSupportController(SupportCustommerRepository SupportCustommerRepository)
        {
            _SupportCustommerRepository = SupportCustommerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeSupportDto>>> GetEmployeeSupports()
        {
            var employeeSupports = await _SupportCustommerRepository.GetAllAsync();
            var employeeSupportDtos = employeeSupports.Select(es => new EmployeeSupportDto
            {
                Id = es.Id,
                CustomerId = es.CustomerId,
             
                // Các thuộc tính khác nếu có
            }).ToList();

            return Ok(employeeSupportDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeSupportDto>> GetEmployeeSupport(int id)
        {
            var employeeSupport = await _SupportCustommerRepository.GetByIdAsync(id);

            if (employeeSupport == null)
            {
                return NotFound(new GeneralReponse(false, "Not Found"));
            }

            var employeeSupportDto = new EmployeeSupportDto
            {
                Id = employeeSupport.Id,
                CustomerId = employeeSupport.CustomerId,
               
                // Các thuộc tính khác nếu có
            };

            return Ok(employeeSupportDto);
        }

        [HttpPost]
        public async Task<ActionResult> PostEmployeeSupport(EmployeeSupportDto employeeSupportDto)
        {
            var employeeSupport = new EmployeeSupport
            {
                CustomerId = employeeSupportDto.CustomerId,
                CashServiceId = employeeSupportDto.CashServiceId,
                EmployeeId = employeeSupportDto.EmployeeId,
                // Các thuộc tính khác nếu có
            };

            await _SupportCustommerRepository.AddAsync(employeeSupport);

            return Ok(new GeneralReponse(true, "Successfully added"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeSupport(int id, EmployeeSupportDto employeeSupportDto)
        {
            var employeeSupport = await _SupportCustommerRepository.GetByIdAsync(id);

            if (employeeSupport == null)
            {
                return NotFound(new GeneralReponse(false, "Not Found"));
            }

            employeeSupport.EmployeeId = employeeSupportDto.EmployeeId;
          
            // Cập nhật các thuộc tính khác nếu có

            await _SupportCustommerRepository.UpdateAsync(employeeSupport);

            return Ok(new GeneralReponse(true, "Successfully updated"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeSupport(int id)
        {
            var employeeSupport = await _SupportCustommerRepository.GetByIdAsync(id);

            if (employeeSupport == null)
            {
                return NotFound(new GeneralReponse(false, "Not Found"));
            }

            await _SupportCustommerRepository.DeleteAsync(id);

            return Ok(new GeneralReponse(true, "Successfully deleted"));
        }
    }
}

