using Aplication.Contracts;
using Aplication.DTOS.Employee.DTOs;
using Aplication.DTOS.Service.DTO;
using Aplication.Responses;
using Domain.Entities.Entitie.Employee;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using static Aplication.DTOS.Service.DTO.ServiceDto;

namespace INITERNAL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly ICloudinaryInterface _cloudinaryInterface;

        public ServiceController(IServiceRepository serviceRepository, ICloudinaryInterface cloudinaryInterface)
        {
            _serviceRepository = serviceRepository;
            _cloudinaryInterface = cloudinaryInterface;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetServices()
        {
            var services = await _serviceRepository.GetAllAsync();
            var serviceDtos = services.Select(s => new ServiceDtos
            {
                Id = s.Id,
                Name = s.ServiceName,
                Description = s.Description,
                Price = s.Price,
                Status = s.Status,
                Image = s.Image,
            }).ToList();

            return Ok(serviceDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDto>> GetService(int id)
        {
            var service = await _serviceRepository.GetByIdAsync(id);

            if (service == null)
            {
                return NotFound(new GeneralReponse(false, "Not Found"));
            }

            var serviceDto = new ServiceDtos
            {
                Id = service.Id,
                Name = service.ServiceName,
                Price = service.Price,
                Description = service.Description,
                Status = service.Status,
                Image = service.Image,
            };

            return Ok(serviceDto);
        }

        [HttpPost]
        public async Task<IActionResult> PostService([FromForm] Service service, IFormFile? file)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (file != null && file.Length > 0)
            {
                try
                {
                    var fileKey = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var imageUrl = await _cloudinaryInterface.UploadImageAsync(file, "services-photos/" + fileKey);
                    service.Image = imageUrl;
                }
                catch (Exception ex)
                {
                    return BadRequest(new GeneralReponse(false, "Image upload failed."));
                }
            }

            try
            {
                var resul = await _serviceRepository.AddAsync(service);
                return Ok(resul);
            }
            catch (Exception ex)
            {
                // Log the exception details for debugging
                Console.WriteLine($"Failed to add employee: {ex.Message}");
                Console.WriteLine($"Inner exception: {ex.InnerException?.Message}");
                return BadRequest(new GeneralReponse(false, "Failed to add employee. Please see inner exception for details."));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutService([FromForm] Service service, IFormFile? file)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (file != null && file.Length > 0)
            {
                try
                {
                    var fileKey = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var imageUrl = await _cloudinaryInterface.UploadImageAsync(file, "services-photos/" + fileKey);
                    service.Image = imageUrl;
                }
                catch (Exception)
                {
                    return BadRequest(new GeneralReponse(false, "Image upload failed"));
                }
            }

            try
            {
                var result = await _serviceRepository.UpdateAsync(service);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new GeneralReponse(false, $"{ex.Message}"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var resul = await _serviceRepository.DeleteAsync(id);
            return Ok(resul);
        }
    }
}
