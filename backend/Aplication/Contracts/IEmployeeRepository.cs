using Aplication.DTOS.Employee.DTOs;
using Aplication.Responses;
using Domain.Entities.Entitie.Employee;

namespace Aplication.Contracts
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAll();
        Task<Employee> GetEmployeeById(int id);

        Task<Employee> Add(Employee employee);
        Task<GeneralReponse> Update(EmployeeDto employeeDto);

        Task<GeneralReponse> Delete(int id);

        Task<IEnumerable<Employee>> GetAllNotStaff();
    }
}
