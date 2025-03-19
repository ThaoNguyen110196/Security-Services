using Aplication.Contracts;
using Domain.Entities.Entitie.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INITERNAL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OvertimeType(IGennericRepository<OvertimeType> genericRepository)
        : GenericControlle<OvertimeType>(genericRepository)
    {
    }
}
