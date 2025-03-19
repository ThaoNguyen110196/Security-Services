using System.Collections;
using System.Security.Claims;
using System.Transactions;
using Aplication.Contracts;
using Aplication.DTOS.Employee.DTOs;
using Aplication.Responses;
using CloudinaryDotNet;
using Domain.Entities.Entitie.Employee;
using Infrastruture.Data;
using Infrastruture.Implementtations;
using Infrastruture.Migrations;
using Infrastruture.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace INITERNAL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICloudinaryInterface _cloudinaryService;
        private readonly IEmployeeSupportRepository _employeeSupportRepository;
      
        private readonly AplicationContext _context;
        // Chỉnh sửa tên interface

        public EmployeeController(IEmployeeRepository employeeRepository,
            ICloudinaryInterface cloudinaryService, 
            AplicationContext context,
            IEmployeeSupportRepository employeeSupportRepository
          )
        {
            _employeeRepository = employeeRepository;
            _cloudinaryService = cloudinaryService;
            _context = context;
            _employeeSupportRepository = employeeSupportRepository;
          

        }

        /// <summary>
        /// [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        /// </summary>
        /// <returns></returns>
        [HttpGet("all/employee-profile")]
        public async Task<IActionResult> GetDetailEmployee()
        {
            // Lấy ID người dùng từ claims và chuyển đổi thành int
            var employeeIdString = User.FindFirstValue("EmployeeId");
            if (string.IsNullOrEmpty(employeeIdString) || !int.TryParse(employeeIdString, out var userId))
            {
                return BadRequest("ID người dùng không hợp lệ.");
            }

            // Lấy tất cả nhân viên từ repository
            var employees = await _employeeRepository.GetAll();
            if (employees == null || !employees.Any())
            {
                return BadRequest("Không tìm thấy nhân viên nào.");
            }

            // Tìm nhân viên hiện tại dựa trên ID người dùng
            var currentEmployee = employees.FirstOrDefault(e => e.Id == userId);
            if (currentEmployee == null)
            {
                return BadRequest("Nhân viên không tìm thấy.");
            }

            // Nếu nhân viên là Giám đốc, trả về tất cả nhân viên
            if (currentEmployee.IsDirector)
            {
                var list = await _employeeRepository.GetAll();
                var employeeDtos = CreateEmployeeDtos(list);
                return Ok(employeeDtos);
            }
            // Nếu nhân viên là Trưởng phòng, chỉ trả về nhân viên trong cùng phòng
            else if (currentEmployee.IsHeadOfDepartment)
            {
                var branchId = currentEmployee.BranchId;
                var employeesInBranch = employees
                     .Where(e => e.BranchId == branchId)
                     .ToList();

                //var employeeDtos = CreateEmployeeDtos(employeesInBranch);
                return Ok(employeesInBranch);
            }
            // Nếu nhân viên không phải Giám đốc hoặc Trưởng phòng, chỉ trả về nhân viên trong cùng chi nhánh
            else
            {
                var branchId = currentEmployee.BranchId;
                if (branchId == null)
                {
                    return BadRequest("ID chi nhánh không có.");
                }

                var employeesInBranch = employees
                    .Where(e => e.BranchId == branchId)
                    .ToList();

                //var employeeDtos = CreateEmployeeDtos(employeesInBranch);
                return Ok(employeesInBranch);
            }
        }

        [HttpGet("get-all-employee")]
        public async Task<IActionResult> GetAllEmployee()
        {
            return Ok(await _employeeRepository.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employees = await _employeeRepository.GetEmployeeById(id);
            if (employees == null) return BadRequest(new GeneralReponse(false, "Employee not found"));
            return Ok(employees);
        }
        private List<EmployeeDto> CreateEmployeeDtos(IEnumerable<Employee> employees)
        {
            return employees.Select(employee => new EmployeeDto
            {


            }).ToList();
        }
        [HttpGet("employee-active")]
        public async Task<IActionResult> GetEmployeesWithoutSupport(int branchId)
        {
            try
            {
                int defaultDepartmentId = 4; // Thay đổi giá trị này thành ID mặc định của bạn

                // Bước 1: Lấy tất cả nhân viên từ repository
                var allEmployees = await _employeeRepository.GetAll();

                // Bước 2: Lọc danh sách nhân viên theo BranchId
                var employeesInBranch = allEmployees
                    .Where(e => e.BranchId == branchId)
                    .ToList();

                // Bước 3: Lọc danh sách nhân viên theo DepartmentId mặc định
                var employeesInBranchAndDepartment = employeesInBranch
                    .Where(e => e.DepartmentId == defaultDepartmentId)
                    .ToList();

                // Bước 4: Lấy tất cả employee IDs từ EmployeeSupport table
                var supportedEmployeeIds = (await _employeeSupportRepository.GetAllAsync())
                    .Select(es => es.EmployeeId)
                    .ToList();

                // Bước 5: Lọc danh sách nhân viên theo điều kiện
                var employeesWithoutSupport = employeesInBranchAndDepartment
                    .Where(e => e.Id != null && !supportedEmployeeIds.Contains(e.Id))
                    .ToList();

                return Ok(employeesWithoutSupport);
            }
            catch (Exception ex)
            {
                // Log the exception and return a generic error response
                // This is just a simple example; consider more specific error handling
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPost("api/insert")]

        public async Task<IActionResult> InsertEmployeeAsync([FromForm] Employee employee, IFormFile? file)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Handle file upload if a file is provided
            if (file != null && file.Length > 0)
            {
                try
                {
                    var fileKey = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var imageUrl = await _cloudinaryService.UploadImageAsync(file, "employee-photos/" + fileKey);
                    employee.Photo = imageUrl;
                   
                }
                catch (Exception ex)
                {
                    return BadRequest(new GeneralReponse(false, "Image upload failed: " + ex.Message));
                }
            }

            try
            {
                // Add employee to the database
                var addedEmployee = await _employeeRepository.Add(employee);

                if (addedEmployee == null)
                {
                    return BadRequest(new GeneralReponse(false, "Failed to add employee."));
                }

                // Check if the employee has a manager and if they are a Director or Head of Department
                if (employee.ManagerId.HasValue)
                {
                    // Kiểm tra sự tồn tại của ManagerId trong bảng Employees
                    var manager = await _context.Managers
                        .FirstOrDefaultAsync(e => e.Id == employee.ManagerId.Value);

                    if (manager == null)
                    {
                        return BadRequest(new GeneralReponse(false, "Manager not found."));
                    }

                    if (employee.IsDirector || employee.IsHeadOfDepartment)
                    {
                        var existingManager = await _context.Managers
                            .FirstOrDefaultAsync(m => m.EmployeeId == employee.ManagerId.Value);

                        if (existingManager == null)
                        {
                            var newManager = new Manager
                            {
                                EmployeeId = addedEmployee.Id,
                                Name = addedEmployee.Name// Use the ID of the newly added employee
                            };

                            _context.Managers.Add(newManager);
                            await _context.SaveChangesAsync();
                        }
                    }
                }

                return Ok(new GeneralReponse(true, "Successfully added employee."));
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                return BadRequest(new GeneralReponse(false, $"An error occurred: {ex.Message}"));
            }
        }


        [HttpPost("upload")]
        public async Task<IActionResult> UploadEmployee([FromForm] EmployeeDto employee, IFormFile? file)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (file != null && file.Length > 0)
            {
                try
                {
                    var fileKey = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var imageUrl = await _cloudinaryService.UploadImageAsync(file, "employee-photos/" + fileKey);
                    employee.Photo = imageUrl;
                }
                catch (Exception)
                {
                    return BadRequest(new GeneralReponse(false, "Image upload failed"));
                }
            }

            try
            {
                var result = await _employeeRepository.Update(employee);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new GeneralReponse(false, $"{ex.Message}"));
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    // Xóa các bản ghi liên quan trong bảng Attendances
                    var attendances = _context.Attendances.Where(a => a.EmployeeId == id);
                    _context.Attendances.RemoveRange(attendances);

                    // Truy vấn và xóa các liên hệ liên quan
                    var contacts = _context.Contracts.Where(c => c.EmployeeId == id);
                    _context.Contracts.RemoveRange(contacts);

                    // Truy vấn và xóa các tài khoản liên quan
                    var accounts = _context.AplictionUsers.Where(a => a.EmployeeId == id);
                    _context.AplictionUsers.RemoveRange(accounts);

                    // Truy vấn và xóa các giờ làm thêm liên quan
                    var overtimes = _context.OverTime.Where(o => o.EmployeeId == id);
                    _context.OverTime.RemoveRange(overtimes);

                    // Truy vấn và xóa các lịch sử đào tạo liên quan
                    var trainingHistories = _context.TrainingHistories.Where(t => t.EmployeeID == id);
                    _context.TrainingHistories.RemoveRange(trainingHistories);

                    // Truy vấn và xóa các lịch trình dịch vụ liên quan
                    var serviceSchedules = _context.ServiceSchedules.Where(s => s.EmployeeID == id);
                    _context.ServiceSchedules.RemoveRange(serviceSchedules);

                    // Truy vấn và xóa các bản ghi trong bảng Sanction trừ những bản ghi có TypeSanction cụ thể
                    var sanctions = _context.Sanctions.Where(s => s.EmployeeId == id);
                    _context.Sanctions.RemoveRange(sanctions);

                    // Truy vấn và xóa bản ghi nhân viên
                    var employee = await _context.Employees.FindAsync(id);
                    if (employee != null)
                    {
                        _context.Employees.Remove(employee);
                    }

                    // Lưu các thay đổi
                    await _context.SaveChangesAsync();

                    // Commit transaction
                    scope.Complete();

                    return Ok(new GeneralReponse(true, "Employee and related records deleted successfully, except specified sanctions"));
                }
                catch (Exception ex)
                {
                    return BadRequest(new GeneralReponse(false, $"An error occurred: {ex.Message}"));
                }
            }
        }

        [HttpGet("get-all-not-staff")]
        public async Task<IActionResult> GetAllNotStaffAsync()
        {
            return Ok(await _employeeRepository.GetAllNotStaff());
        }
    }
}
