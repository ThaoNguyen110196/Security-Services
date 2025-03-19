using Aplication.DTOS.Department.DTOs;
using Infrastruture.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace INITERNAL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerDeparment : ControllerBase
    {

        private readonly AplicationContext _context;

        public ManagerDeparment(AplicationContext context)
        {
            _context = context;
        }

        [HttpGet("api/getMangeAll")]
        public async Task<IActionResult> GetAllmanage()
        {
            var listManages = await _context.Branches.ToListAsync();

            if (listManages == null) return BadRequest(new { Success = false, Message = "Not found" });

            var branchDtos = listManages.Select(m => new BranchDto
            {

                Branch = new BranchDto
                {
                  Id = m.Id,
                  Name = m.Name,
                },
            }).ToList();

            return Ok(branchDtos);
        }

    }
}
