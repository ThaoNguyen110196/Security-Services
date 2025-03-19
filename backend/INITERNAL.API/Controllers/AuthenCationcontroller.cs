using Aplication.Contracts;
using Aplication.DTOS;
using Infrastruture.Implementtations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INITERNAL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenCationcontroller(IUserAccount acountInterface) : ControllerBase
    {

        [HttpPost("register")]
        public async Task<IActionResult> CreateAsync(Register register)
        {
            if (register == null) return BadRequest("model is empty");
            var resul = await acountInterface.CreateAsync(register);
            return Ok(resul);
        }

        [HttpPost("login")]
        public async Task<IActionResult> SingInAsync(Login user)
        {
            if (user == null) return BadRequest("model is empty");
            var result = await acountInterface.SingInAsync(user);
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefershTokenAsync(RefreshToken  token) {
            if (token is null) return BadRequest("model is empty");
            var result = await acountInterface.RefreshTokenAsync(token);
            return Ok(result);
        }
    }
}
