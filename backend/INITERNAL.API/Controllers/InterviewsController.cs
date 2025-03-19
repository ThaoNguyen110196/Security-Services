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
    public class InterviewsController : ControllerBase
    {
        private readonly IInterviewRepository _interviewRepository;

        public InterviewsController(IInterviewRepository interviewRepository)
        {
            _interviewRepository = interviewRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InterviewDto>>> GetInterviews()
        {
            var interviews = await _interviewRepository.GetAllInterviewsAsync();
            var interviewDtos = interviews.Select(i => new InterviewDto
            {
                InterviewID = i.Id,
                CandidateID = i.CandidateID,
                InterviewDate = i.InterviewDate,
                InterviewLocation = i.InterviewLocation,
                InterviewResult = i.InterviewResult,
                Comments = i.Comments,
                Name = i.Name,
            }).ToList();

            return Ok(interviewDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InterviewDto>> GetInterview(int id)
        {
            var interview = await _interviewRepository.GetInterviewByIdAsync(id);

            if (interview == null)
            {
                return NotFound(new GeneralReponse(false, "Not Found"));
            }

            var interviewDto = new InterviewDto
            {
               
                InterviewID = interview.Id,
                CandidateID = interview.CandidateID,
                InterviewDate = interview.InterviewDate,
                InterviewLocation = interview.InterviewLocation,
                InterviewResult = interview.InterviewResult,
                Comments = interview.Comments,
                Name= interview.Name,
            };

            return Ok(interviewDto);
        }
        [HttpGet("get-interview-by-candidate/{candidateId}")]
            public async Task<ActionResult<InterviewDto>> GetInterviewByCandidateId(int candidateId)
        {
            var interview = await _interviewRepository.GetInterviewByCandidateIdAsync(candidateId);

            if (interview == null)
                return NotFound(new GeneralReponse(false, "Not Found"));

            var interviewDto = new InterviewDto
            {
                InterviewID = interview.Id,
                CandidateID = interview.CandidateID,
                InterviewDate = interview.InterviewDate,
                InterviewLocation = interview.InterviewLocation,
                InterviewResult = interview.InterviewResult,
                Comments = interview.Comments,
                Name = interview.Name,
            };

            return Ok(interviewDto);
        }

        [HttpPost]
        public async Task<ActionResult> PostInterview(Interview interview)
        {
            

           var resul = await _interviewRepository.AddInterviewAsync(interview);

            return Ok(resul);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInterview(int id, InterviewDto interviewDto)
        {
            var interview = await _interviewRepository.GetInterviewByIdAsync(id);

            if (interview == null)
            {
                return NotFound(new GeneralReponse(false, "Not Found"));
            }

     
            interview.InterviewDate = (DateTime)interviewDto.InterviewDate!;
            interview.InterviewLocation = interviewDto.InterviewLocation;
            interview.InterviewResult = interviewDto.InterviewResult;
            interview.Comments = interviewDto.Comments;

             var resul =   await _interviewRepository.UpdateInterviewAsync(interview);

            return Ok(resul);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInterview(int id)
        {
           var resul =   await _interviewRepository.DeleteInterviewAsync(id);
            return Ok(resul);
        }
    }
}
