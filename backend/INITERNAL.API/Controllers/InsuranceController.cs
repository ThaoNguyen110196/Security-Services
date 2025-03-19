using Aplication.Contracts;
using Domain.Entities.Entitie.Employee;

using Microsoft.AspNetCore.Mvc;

namespace INITERNAL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceController(IGennericRepository<Insurance> genericRepository)
        : GenericControlle<Insurance>(genericRepository)
    {

    }
    
}
