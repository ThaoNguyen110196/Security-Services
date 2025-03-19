
using Aplication.Contracts;
using Aplication.DTOS.Employee.DTOs;
using Aplication.Responses;
using Domain.Entities.Entitie.Employee;
using Infrastruture.Data;
using Microsoft.EntityFrameworkCore;



namespace Infrastruture.Implementtations
{
    public class EmployeeReponsitory : IEmployeeRepository
    {
        private readonly AplicationContext _context;

        public EmployeeReponsitory(AplicationContext context)
        {
            _context = context;
        }

        public async Task<Employee> Add(Employee employee)
        {
            var result = await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync(); // Ensure changes are committed
            return result.Entity; // Return the added employee entity
        }

        public async Task<GeneralReponse> Update(EmployeeDto employeeDto)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == employeeDto.Id);
            if (employee == null) return new GeneralReponse(false, "Employee not found.");

            employee.Name = employeeDto.Name;
            employee.CivilId = employeeDto.CivilId;
            employee.Gender = employeeDto.Gender;
            employee.Birthday = employeeDto.Birthday;
            employee.FileName = employeeDto.FileName;
            employee.JobName = employeeDto.JobName;
            employee.Address = employeeDto.Address;
            employee.PhoneNumber = employeeDto.PhoneNumber;
            employee.Other = employeeDto.Other;
            employee.EducationId = employeeDto.EducationId;
            employee.BranchId = employeeDto.BranchId;
            employee.CountryId = employeeDto.CountryId;
            employee.ProvinceId = employeeDto.ProvinceId;
            employee.DistrictId = employeeDto.DistrictId;
            employee.IsDirector = employeeDto.IsDirector;
            employee.IsHeadOfDepartment = employeeDto.IsHeadOfDepartment;
            employee.ManagerId = employeeDto.ManagerId;
            employee.PositionId = employeeDto.PositionId;

            if (!string.IsNullOrEmpty(employeeDto.Photo))
                employee.Photo = employeeDto.Photo;

            await Commit();
            return Sucesss();
        }

        public Task<GeneralReponse> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _context.Set<Employee>()
         .Include(e => e.Departnent) // Include the related Department entity
         .ToListAsync();
        }

        private async Task<bool> CheckCivilId(string civilId)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.CivilId.ToLower() == civilId.ToLower());
            return employee == null;
        }

        public static GeneralReponse NotFound() => new(false, "Sorry, an employee with this CivilId already exists.");
        public static GeneralReponse Sucesss() => new(true, "Process completd");

        private async Task Commit() => await _context.SaveChangesAsync();

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<IEnumerable<Employee>> GetAllNotStaff()
        {
            return await _context.Employees
                    .Where(x => x.Position != null && x.Position.Name != "Staff")
                    .ToListAsync();
        }
    }
}
