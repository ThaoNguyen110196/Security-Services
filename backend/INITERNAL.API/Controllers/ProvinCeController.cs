using Aplication.Contracts;
using Aplication.Responses;
using Domain.Entities.Entitie.Employee;
using Infrastruture.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INITERNAL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinCeController(IGennericRepository<Province> genericRepository)
        : GenericControlle<Province>(genericRepository)
    {
        [HttpGet("ShowProvinceByCountry")]
        public async Task<IActionResult> GetProvinceByCountry(int id)
        {
            var provinces = await genericRepository.GetAll();
            if (provinces is null) return  BadRequest(new GeneralReponse(false, "No data found"));
            var provincesByCountry = provinces.Where(p => p.CountryId == id).ToList();
            if (provincesByCountry is null) return NotFound(new GeneralReponse(false, "No provinces found for this country"));
            return Ok(provincesByCountry);

        }
    }
}
