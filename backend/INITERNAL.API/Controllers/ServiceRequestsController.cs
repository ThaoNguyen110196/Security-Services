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
    public class ServiceRequestsController : ControllerBase
    {
        private readonly IServiceRequestRepository _serviceRequestRepository;

        public ServiceRequestsController(IServiceRequestRepository serviceRequestRepository)
        {
            _serviceRequestRepository = serviceRequestRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceRequestDto>>> GetServiceRequests()
        {
            var serviceRequests = await _serviceRequestRepository.GetAllServiceRequestsAsync();
            var serviceRequestsDto = serviceRequests.Select(sr => new ServiceRequestDto
            {
                ServiceName = sr.Name,
                ServiceRequestID = sr.Id,
                CustomerID = sr.Customer.Id,
                ServiceType = sr.ServiceType,
                RequestDetails = sr.RequestDetails,
                Status = sr.Status
            }).ToList();

            return Ok(serviceRequestsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceRequestDto>> GetServiceRequest(int id)
        {
            var serviceRequest = await _serviceRequestRepository.GetServiceRequestByIdAsync(id);

            if (serviceRequest is null) return BadRequest(new GeneralReponse(false, "Not Found"));

            var serviceRequestDto = new ServiceRequestDto
            {
                ServiceRequestID = serviceRequest.Id,
                CustomerID = serviceRequest.Customer.Id,
                ServiceType = serviceRequest.ServiceType,
                RequestDetails = serviceRequest.RequestDetails,
                Status = serviceRequest.Status
            };

            return Ok(serviceRequestDto);
        }

        [HttpPost]
        public async Task<ActionResult> PostServiceRequest(ServiceRequest serviceRequest)
        {
           

            var resul =   await _serviceRequestRepository.AddServiceRequestAsync(serviceRequest);

            return Ok(resul);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceRequest( ServiceRequest serviceRequest)
        {
           
           var resul =  await _serviceRequestRepository.UpdateServiceRequestAsync(serviceRequest);

            return Ok(resul);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceRequest(int id)
        {
              var resul =  await _serviceRequestRepository.DeleteServiceRequestAsync(id);
            return Ok(resul);
        }
    }
}

