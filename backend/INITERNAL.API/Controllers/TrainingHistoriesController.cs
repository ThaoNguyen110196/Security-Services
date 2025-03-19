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
    public class TrainingHistoriesController : ControllerBase
    {
        private readonly ITrainingHistoryRepository _trainingHistoryRepository;

        public TrainingHistoriesController(ITrainingHistoryRepository trainingHistoryRepository)
        {
            _trainingHistoryRepository = trainingHistoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingHistoryDto>>> GetTrainingHistories()
        {
            var trainingHistories = await _trainingHistoryRepository.GetAllTrainingHistoriesAsync();
            var trainingHistoriesDto = trainingHistories.Select(th => new TrainingHistoryDto
            {
                TrainingID = th.Id,
                ProgramID = th.ProgramID,
                Name = th.Name,
                EmployeeID = th.EmployeeID,
                CompletionStatus = th.CompletionStatus,
                CompletionDate = th.CompletionDate
            }).ToList();

            return Ok(trainingHistoriesDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingHistoryDto>> GetTrainingHistory(int id)
        {
            var trainingHistory = await _trainingHistoryRepository.GetTrainingHistoryByIdAsync(id);

            if (trainingHistory is null)
                return BadRequest(new GeneralReponse(false, "Not Found"));

            var trainingHistoryDto = new TrainingHistoryDto
            {
                TrainingID = trainingHistory.Id,
                ProgramID = trainingHistory.ProgramID,
                EmployeeID = trainingHistory.EmployeeID,
                CompletionStatus = trainingHistory.CompletionStatus,
                CompletionDate = trainingHistory.CompletionDate
            };

            return Ok(trainingHistoryDto);
        }

        [HttpPost]
        public async Task<ActionResult> PostTrainingHistory(TrainingHistory trainingHistory)
        {
            

            await _trainingHistoryRepository.AddTrainingHistoryAsync(trainingHistory);

            return Ok(new GeneralReponse(true, "Successfully"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainingHistory( TrainingHistory trainingHistory)
        {
            

             var resul =   await _trainingHistoryRepository.UpdateTrainingHistoryAsync(trainingHistory);

            return Ok(resul);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainingHistory(int id)
        {
              var resul  =  await _trainingHistoryRepository.DeleteTrainingHistoryAsync(id);
            return Ok(resul);
        }
    }
}
