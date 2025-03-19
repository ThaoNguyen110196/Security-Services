using Aplication.Contracts;
using Aplication.Responses;
using Domain.Entities.Entitie.Service;
using Infrastruture.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Aplication.DTOS.Service.DTO.ServiceDto;

namespace INITERNAL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPositionsController : ControllerBase
    {
        private readonly IJobPositionRepository _jobPositionRepository;

        public JobPositionsController(IJobPositionRepository jobPositionRepository)
        {
            _jobPositionRepository = jobPositionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobPositionDto>>> GetJobPositions()
        {
            var jobPositions = await _jobPositionRepository.GetAllJobPositionsAsync();
            var jobPositionsDto = jobPositions.Select(jp => new JobPositionDto
            {
                PositionID = jp.Id,
                PositionName = jp.PositionName,
                JobRequirements = jp.JobRequirements,
                JobDescription = jp.JobDescription,
                ApplicationDeadline = jp.ApplicationDeadline,
                 Status = jp.Status
            }).ToList();

            return Ok(jobPositionsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobPositionDto>> GetJobPosition(int id)
        {
            var jobPosition = await _jobPositionRepository.GetJobPositionByIdAsync(id);

            if (jobPosition is null) return BadRequest(new GeneralReponse(false, "Not Found"));

            var jobPositionDto = new JobPositionDto
            {
                PositionID = jobPosition.Id,
                PositionName = jobPosition.PositionName,
                JobRequirements = jobPosition.JobRequirements,
                JobDescription = jobPosition.JobDescription,
                ApplicationDeadline = jobPosition.ApplicationDeadline,
               Status = jobPosition.Status
            };

            return Ok(jobPositionDto);
        }

        [HttpPost]
        public async Task<ActionResult> PostJobPosition(JobPosition jobPosition)
        {
           

            await _jobPositionRepository.AddJobPositionAsync(jobPosition);

            return Ok(new GeneralReponse(true, "Successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobPosition( JobPosition jobPosition)
        {
            

           var resul =  await _jobPositionRepository.UpdateJobPositionAsync(jobPosition);

            return Ok(resul);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobPosition(int id)
        {
            var resul =   await _jobPositionRepository.DeleteJobPositionAsync(id);
            return Ok(resul);
        }
    }
}
