using Aplication.Contracts;
using Aplication.Responses;
using Domain.Entities.Entitie.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INITERNAL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController(IGennericRepository<District> genericRepository)
        : GenericControlle<District>(genericRepository)
    {
        [HttpGet("ShowDistrictByProvince")]
        public async Task<IActionResult> GetProvinceByCountry(int id)
        {
            var district = await genericRepository.GetAll();
            if (district is null) return BadRequest(new GeneralReponse(false, "No data found"));
            var provincesByCountry = district.Where(p => p.ProvinceId == id).ToList();
            if (provincesByCountry is null) return NotFound(new GeneralReponse(false, "No provinces found for this country"));
            return Ok(provincesByCountry);

        }
    }
}
