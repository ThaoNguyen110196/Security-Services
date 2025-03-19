using Aplication.Contracts;
using Domain.Entities.Entitie.Employee;

namespace INITERNAL.API.Controllers
{
    public class OvertimeTypeController(IGennericRepository<OvertimeType> genericRepository)
        : GenericControlle<OvertimeType>(genericRepository)
    {
    }
}
