using System.Linq;
using Aplication.Contracts;
using Aplication.DTOS;
using Aplication.Responses;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Entities.Entitie.Service;
using Infrastruture.Migrations;
using Infrastruture.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Aplication.DTOS.Service.DTO.ServiceDto;

namespace INITERNAL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly ICloudinaryInterface _cloudinaryInterface;
        private readonly IGoogleDriveService _googleDriveService;
        private readonly IMailRepository _mailRepository;
        private readonly TokenService _tokenService;


        public CandidatesController(ICandidateRepository candidateRepository, TokenService tokenService, IMailRepository mailRepository,
            ICloudinaryInterface cloudinaryInterface,
            IGoogleDriveService googleDriveService)
        {
            _candidateRepository = candidateRepository;
            _cloudinaryInterface = cloudinaryInterface;
            _googleDriveService = googleDriveService;
            _mailRepository = mailRepository;
            _tokenService = tokenService;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Candidate>>> GetCandidates()
        {
            var candidates = await _candidateRepository.GetAllCandidatesAsync();


            return Ok(candidates);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Candidate>> GetCandidate(int id)
        {
            var candidate = await _candidateRepository.GetCandidateByIdAsync(id);



            return Ok(candidate);
        }

        [HttpPost]
        public async Task<IActionResult> PostCandidate([FromForm] Candidate candidate, IFormFile cvFile)
        {
            string cvFileUrl = null;
            string cvFileId = null;

            try
            {
                // Kiểm tra tệp đính kèm
                if (cvFile != null && cvFile.Length > 0)
                {
                    // Kiểm tra định dạng tệp
                    var allowedExtensions = new[] { ".pdf" };
                    var fileExtension = Path.GetExtension(cvFile.FileName).ToLower();
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        return BadRequest(new GeneralReponse(false, "Only PDF files are allowed"));
                    }

                    // Kiểm tra kích thước tệp (ví dụ: không vượt quá 5MB)
                    const long maxFileSize = 5 * 1024 * 1024; // 5MB
                    if (cvFile.Length > maxFileSize)
                    {
                        return BadRequest(new GeneralReponse(false, "File size exceeds the maximum limit of 5MB"));
                    }

                    // Tải tệp lên Google Drive
                    var folderName = "19f4Q9guoIwUM778M48ich6Q_mY4Pbdy5";
                    cvFileId = await _googleDriveService.UploadFileAsync(cvFile, folderName);

                    // Lấy URL tệp từ Google Drive
                    cvFileUrl = $"https://drive.google.com/uc?id={cvFileId}";
                }

                // Kiểm tra thông tin ứng viên
                if (string.IsNullOrEmpty(candidate.Email))
                {
                    if (!string.IsNullOrEmpty(cvFileId))
                    {
                        // Xóa tệp từ Google Drive nếu email không có
                        await _googleDriveService.DeleteFileAsync(cvFileId);
                    }
                    return BadRequest(new GeneralReponse(false, "Candidate email is required"));
                }

                // Kiểm tra tính hợp lệ của email
                if (!IsValidEmail(candidate.Email))
                {
                    if (!string.IsNullOrEmpty(cvFileId))
                    {
                        await _googleDriveService.DeleteFileAsync(cvFileId);
                    }
                    return BadRequest(new GeneralReponse(false, "Invalid email format"));
                }

                // Kiểm tra các trường bắt buộc khác
                if (string.IsNullOrEmpty(candidate.Name))
                {
                    return BadRequest(new GeneralReponse(false, "Candidate name is required"));
                }

                if (string.IsNullOrEmpty(candidate.Phone))
                {
                    return BadRequest(new GeneralReponse(false, "Candidate phone number is required"));
                }

                // Tạo đối tượng ứng viên và lưu vào cơ sở dữ liệu
                candidate.CvFilePath = cvFileUrl; // Lưu URL của tệp CV
                var result = await _candidateRepository.AddCandidateAsync(candidate);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new GeneralReponse(false, $"Failed to upload CV: {ex.Message}"));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCandidate(Candidate candidate)
        {


            var resul = await _candidateRepository.UpdateCandidateAsync(candidate);

            return Ok(resul);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCandidate(int id)
        {
            var resul = await _candidateRepository.DeleteCandidateAsync(id);
            return Ok(resul);
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        [HttpPost("send-mail-approve/{id}")]
        public async Task<IActionResult> SendApproveMail(int id)
        {
            try
            {
                var candidate = await _candidateRepository.GetCandidateByIdAsync(id);
                if (candidate == null) return NotFound("Candidate not found.");

                var token = _tokenService.GenerateEmailTokenForCandidate(candidate);

                SaveTokenToDatabase(candidate.Id, token);

                await _mailRepository.SendMailInterviewsAsync(candidate.Email, "approve", token);
                return Ok("Success.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("send-mail-reject/{id}")]
        public async Task<IActionResult> SendRejectMail(int id)
        {
            try
            {
                var candidate = await _candidateRepository.GetCandidateByIdAsync(id);
                if (candidate == null) return NotFound("Candidate not found.");

                await _mailRepository.SendMailInterviewsAsync(candidate.Email, "reject", null);
                return Ok("Success.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private void SaveTokenToDatabase(int customerId, string token)
        {
            var tokenRecord = new EmailTokenDto
            {
                ClientId = customerId,
                Token = token,
                ExpirationDate = DateTime.UtcNow.AddHours(1),
                IsUsed = false
            };
        }

    }
}
