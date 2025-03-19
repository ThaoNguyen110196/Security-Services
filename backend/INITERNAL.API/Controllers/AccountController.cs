using Aplication.DTOS.Employee.DTOs;
using Infrastruture.Data;
using Infrastruture.Implementtations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace INITERNAL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AplicationContext _context;

        public AccountController(AplicationContext context)
        {
            _context = context;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAccounts()
        {
            // Get the list of accounts from the repository/service
            var accounts = await _context.AplictionUsers.ToArrayAsync();
            // Return the list of accounts with an OK status
            return Ok(accounts);
        }

        [HttpGet("not-create")]
        public async Task<IActionResult> GetAllAccountNotCreate()
        {
            var existingEmployeeIds = await _context.AplictionUsers
                .Select(u => u.EmployeeId)
                .ToListAsync();

            var employeesWithoutAccount = await _context.Employees
                .Where(e => !existingEmployeeIds.Contains(e.Id))
                .ToListAsync();

            return Ok(employeesWithoutAccount);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var account = await _context.AplictionUsers.FindAsync(id);
            if (account == null) NotFound();

            _context.AplictionUsers.Remove(account);
            await _context.SaveChangesAsync();
            return Ok("success");
        }
    }
}
