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
    public class ServiceSchedulesController : ControllerBase
    {
        private readonly IServiceScheduleRepository _serviceScheduleRepository;

        public ServiceSchedulesController(IServiceScheduleRepository serviceScheduleRepository)
        {
            _serviceScheduleRepository = serviceScheduleRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceScheduleDto>>> GetServiceSchedules()
        {
            var schedules = await _serviceScheduleRepository.GetAllServiceSchedulesAsync();
            var schedulesDto = schedules.Select(s => new ServiceScheduleDto
            {
                ScheduleID = s.Id,
                Name = s.Name,
                ServiceRequestID = (int)s.ServiceRequestID!,
                EmployeeID = (int)s.EmployeeID!,
                ScheduledDate = (DateTime)s.ScheduledDate!,
                Location = s.Location
            }).ToList();

            return Ok(schedulesDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceScheduleDto>> GetServiceSchedule(int id)
        {
            var schedule = await _serviceScheduleRepository.GetServiceScheduleByIdAsync(id);

            if (schedule == null)
            {
                return NotFound();
            }

            var scheduleDto = new ServiceScheduleDto
            {
                ScheduleID = schedule.Id,
                ServiceRequestID = schedule.ServiceRequestID,
                EmployeeID = schedule.EmployeeID,
                ScheduledDate = schedule.ScheduledDate,
                Location = schedule.Location
            };

            return Ok(scheduleDto);
        }

        [HttpPost]
        public async Task<ActionResult> PostServiceSchedule(ServiceSchedule schedule)
        {
            

            var resul =    await _serviceScheduleRepository.AddServiceScheduleAsync(schedule);

            return Ok(resul);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceSchedule( ServiceSchedule schedule)
        {
           

         var resul =      await _serviceScheduleRepository.UpdateServiceScheduleAsync(schedule);

            return Ok(resul);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceSchedule(int id)
        {
                var resul = await _serviceScheduleRepository.DeleteServiceScheduleAsync(id);
            return Ok(resul);
        }
    }
}
