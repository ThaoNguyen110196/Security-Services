using Aplication.Contracts;
using Domain.Entities.Entitie.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INITERNAL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationController(IGennericRepository<Vacation> genericRepository) 
        : GenericControlle<Vacation>(genericRepository)
    {
    }
}
