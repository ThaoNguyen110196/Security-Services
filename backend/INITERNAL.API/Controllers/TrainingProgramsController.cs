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
    public class TrainingProgramsController : ControllerBase
    {
        private readonly ITrainingProgramRepository _trainingProgramRepository;

        public TrainingProgramsController(ITrainingProgramRepository trainingProgramRepository)
        {
            _trainingProgramRepository = trainingProgramRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingProgramDto>>> GetTrainingPrograms()
        {
            var trainingPrograms = await _trainingProgramRepository.GetAllTrainingProgramsAsync();
            var trainingProgramsDto = trainingPrograms.Select(tp => new TrainingProgramDto
            {
                ProgramID = tp.Id,
                ProgramName = tp.Name,
                Description = tp.Description,
                StartDate = tp.StartDate,
                EndDate = tp.EndDate,
                Objectives = tp.Objectives,
                Instructor = tp.Instructor
            }).ToList();

            return Ok(trainingProgramsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingProgramDto>> GetTrainingProgram(int id)
        {
            var trainingProgram = await _trainingProgramRepository.GetTrainingProgramByIdAsync(id);

            if (trainingProgram is null) return BadRequest(new GeneralReponse(false, "Not Found"));

            var trainingProgramDto = new TrainingProgramDto
            {
                ProgramID = trainingProgram.Id,
                ProgramName = trainingProgram.Name,
                Description = trainingProgram.Description,
                StartDate = trainingProgram.StartDate,
                EndDate = trainingProgram.EndDate,
                Instructor = trainingProgram.Instructor
            };

            return Ok(trainingProgramDto);
        }

        [HttpPost]
        public async Task<ActionResult> PostTrainingProgram(TrainingProgram trainingProgram)
        {
            
            var resul =    await _trainingProgramRepository.AddTrainingProgramAsync(trainingProgram);

            return Ok(resul);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainingProgram( TrainingProgram trainingProgram)
        {
         


           var resul  =   await _trainingProgramRepository.UpdateTrainingProgramAsync(trainingProgram);

            return Ok(resul);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainingProgram(int id)
        {
            var resul =  await _trainingProgramRepository.DeleteTrainingProgramAsync(id);
            return Ok(resul);
        }
    }
}
