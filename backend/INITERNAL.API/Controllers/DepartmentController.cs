using Aplication.Contracts;
using Aplication.DTOS.Department.DTOs;
using Aplication.Responses;
using Domain.Entities.Entitie.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace INITERNAL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController(IGennericRepository<Departnent> genericRepository) : GenericControlle<Departnent>(genericRepository)
    {

       
    }
}
