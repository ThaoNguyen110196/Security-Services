using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Responses;
using Domain.Entities.Entitie.Employee;

namespace Aplication.Contracts
{
    public interface IEmployeeSupportRepository
    {
        Task<GeneralReponse> AddAsync(EmployeeSupport employeeSupport);
        Task<GeneralReponse> DeleteAsync(int id);
        Task<IEnumerable<EmployeeSupport>> GetAllAsync();
        Task<EmployeeSupport> GetByIdAsync(int id);
        Task<GeneralReponse> UpdateAsync(EmployeeSupport employeeSupport);
        Task<IEnumerable<EmployeeSupport>> GetCustomerSupportsByCustomerIdAsync(int customerId);
    }
}
