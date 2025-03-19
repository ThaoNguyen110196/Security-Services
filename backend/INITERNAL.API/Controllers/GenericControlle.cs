using System.Text.Json.Serialization;
using System.Text.Json;
using Aplication.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Infrastruture.Data;

namespace INITERNAL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericControlle<T>(IGennericRepository<T> gennericRepository) : ControllerBase where T : class
    {


        

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var employees = await gennericRepository.GetAll();

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(employees, options);
            return Content(json, "application/json");
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest("Invalid request sent");
            return Ok(await gennericRepository.Delete(id));
        }

        [HttpGet ("single/{id}")]
        
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0) return BadRequest("Invalid request sent");
            return Ok( await gennericRepository.GetById(id));

        }
        [HttpPost("update")]
        public async Task<IActionResult>Update(T model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok( await gennericRepository.Update(model));
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add(T model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await gennericRepository.Inser(model));
        }
      

    }
}
