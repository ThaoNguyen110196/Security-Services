using Aplication.Contracts;
using Aplication.DTOS.Department.DTOs;
using Aplication.Responses;
using Domain.Entities.Entitie.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace INITERNAL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrachController(IGennericRepository<Branch> genericRepository)
        : GenericControlle<Branch>(genericRepository)
    {
    }
}
