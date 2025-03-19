using Aplication.Contracts;
using Infrastruture.Data;
using Microsoft.AspNetCore.Mvc;

namespace INITERNAL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController(IGennericRepository<Position> genericRepository)
        : GenericControlle<Position>(genericRepository)
    {
    }
}
