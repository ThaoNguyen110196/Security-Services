using System.Transactions;
using Aplication.Contracts;
using Aplication.DTOS;
using Aplication.DTOS.Employee.DTOs;
using Aplication.Responses;
using Domain.Entities.Entitie.Employee;
using Infrastruture.Services;
using Microsoft.AspNetCore.Mvc;
using static Aplication.DTOS.Service.DTO.ServiceDto;

namespace INITERNAL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmployeeSupportRepository _employeeSupportRepository;
        private readonly IServiceRequestRepository _serviceRequestRepository;
        private readonly ICloudinaryInterface _cloudinaryInterface;
        private readonly ILogServiceEntity _logService;
        private readonly IMailRepository _mailRepository;
        private readonly TokenService _tokenService;

        public CustomersController(ICustomerRepository customerRepository,
                                  ICloudinaryInterface cloudinaryInterface,
                                  IEmployeeSupportRepository employeeSupportRepository,
                                  ILogServiceEntity logService,
                                  IServiceRequestRepository serviceRequestRepository, IMailRepository mailRepository, TokenService tokenService)
        {
            _customerRepository = customerRepository;
            _cloudinaryInterface = cloudinaryInterface;
            _employeeSupportRepository = employeeSupportRepository;
            _logService = logService;
            _serviceRequestRepository = serviceRequestRepository;
            _mailRepository = mailRepository;
            _tokenService = tokenService;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();
            var customersDto = customers.Select(c => new CustomerDto
            {
                CustomerID = c.Id,
                CustomerName = c.Name,
                Email = c.Email,
                Phone = c.Phone,
                Image = c.Image,
            }).ToList();

            return Ok(customersDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);

            if (customer is null) return BadRequest(new GeneralReponse(false, "Not Found"));




            var customerDto = new CustomerDto
            {
                CustomerID = customer.Id,
                CustomerName = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone
            };

            return Ok(customerDto);
        }

        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromForm] CustomerEmployeeSupportDto customerDto, IFormFile file)
        {
            if (customerDto == null) return BadRequest(new GeneralReponse(false, "Customer data is required"));

            string imageUrl = null;

            if (file != null && file.Length > 0)
            {
                try
                {
                    if (_cloudinaryInterface == null)
                    {
                        return BadRequest(new GeneralReponse(false, "Cloudinary service is not initialized"));
                    }

                    var fileKey = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    imageUrl = await _cloudinaryInterface.UploadImageAsync(file, "customer-photos/" + fileKey);
                    customerDto.Image = imageUrl;

                    if (string.IsNullOrEmpty(customerDto.Name))
                    {
                        await _cloudinaryInterface.DeleteImageAsync(imageUrl);
                        return BadRequest(new GeneralReponse(false, "Customer name is required"));
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(new GeneralReponse(false, $"Failed to upload image: {ex.Message}"));
                }
            }
            else
            {
                if (string.IsNullOrEmpty(customerDto.Name))
                {
                    return BadRequest(new GeneralReponse(false, "Customer name is required"));
                }
            }

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    // Map DTO to Customer entity
                    var customer = new Customer
                    {
                        Name = customerDto.Name,
                        Email = customerDto.Email,
                        Phone = customerDto.Phone,
                        Image = customerDto.Image
                        // Add other necessary property mappings
                    };

                    // Save customer to repository
                    var result = await _customerRepository.AddCustomerAsync(customer);

                    if (result == null)
                    {
                        if (!string.IsNullOrEmpty(imageUrl))
                        {
                            await _cloudinaryInterface.DeleteImageAsync(imageUrl);
                        }
                        return BadRequest(new GeneralReponse(false, "Failed to save customer"));
                    }

                    // Create EmployeeSupport entries if necessary
                    if (customerDto.EmployeeIds != null && customerDto.EmployeeIds.Any())
                    {
                        foreach (var employeeId in customerDto.EmployeeIds)
                        {
                            var employeeSupport = new EmployeeSupport
                            {
                                EmployeeId = employeeId,
                                CustomerId = result.Id,
                                CashServiceId = customerDto.CashServiceId,
                                ServiceId = customerDto.ServiceId
                                // Add other necessary property mappings
                            };

                            await _employeeSupportRepository.AddAsync(employeeSupport);
                        }
                    }

                    // Create ServiceSupport entries if necessary


                    // Complete the transaction
                    scope.Complete();

                    return Ok(new GeneralReponse(true, "successfully"));
                }
                catch (Exception ex)
                {
                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        await _cloudinaryInterface.DeleteImageAsync(imageUrl);
                    }
                    return BadRequest(new GeneralReponse(false, $"Failed to save customer and supports: {ex.Message}"));
                }
            }
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> PutCustomera(Customer customer)
        {
            var resul = await _customerRepository.UpdateCustomerAsync(customer);
            return Ok(resul);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                // Lấy khách hàng và các bản ghi liên quan
                var customer = await _customerRepository.GetCustomerByIdAsync(id);
                var customerSupports = await _employeeSupportRepository.GetCustomerSupportsByCustomerIdAsync(id);
                var serviceRequests = await _serviceRequestRepository.GetServiceRequestsByCustomerIdAsync(id);

                if (customer == null)
                    return NotFound(new GeneralReponse(false, "Customer not found"));

                if (customerSupports == null)
                    return NotFound(new GeneralReponse(false, "Customer supports not found"));

                if (serviceRequests == null)
                    return NotFound(new GeneralReponse(false, "Service requests not found"));

                // Ghi log thông tin khách hàng, các hỗ trợ nhân viên và các yêu cầu dịch vụ trước khi xóa
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    // Ghi log thông tin khách hàng
                    await _logService.LogDeletionAsync(customer, $"Customer details: Name: {customer.Name}", DateTime.UtcNow);

                    // Ghi log thông tin EmployeeSupports
                    foreach (var support in customerSupports)
                    {
                        var details = $"Customer details: Id: {id}\n" +

                             "Employee Supports:\n" +
                             string.Join("\n", customerSupports.Select(es =>
                             $"EmployeeSupport - ID: {es.Id}, " +
                             $"CashService Price: {(es.CashService != null ? es.CashService.Price.ToString() : "N/A")}, " +
                             $"ServiceId: {es.ServiceId}, " +
                             $"CustomerId: {es.CustomerId}, " +
                             $"Customer Name: {(es.Customer != null ? es.Customer.Name : "N/A")}, " +
                             $"ImageCustomer: {(es.Customer != null ? es.Customer.Image : "N/A")}, " +
                             $"Service Price: {(es.Service != null ? es.Service.Price.ToString() : "N/A")}"
   ));

                   


                        await _logService.LogDeletionAsync(customerSupports, details, DateTime.UtcNow);



                        await _employeeSupportRepository.DeleteAsync(support.Id);

                    }


                    // Ghi log thông tin ServiceRequests
                    foreach (var request in serviceRequests)
                    {
                        await _logService.LogDeletionAsync(request,
                            $"ServiceRequest details: ServiceId: {request.Id}," +
                            $" Date: {request.CustomerID}," +
                            $" Description: {request.RequestDetails}" +
                             $" Description: {request.ServiceType}"

                            , DateTime.UtcNow);
                        await _serviceRequestRepository.DeleteServiceRequestAsync(request.Id);

                    }

                   

                    // Xóa khách hàng
                    await _customerRepository.DeleteCustomerAsync(id);

                    // Commit giao dịch
                    scope.Complete();

                    return Ok(new GeneralReponse(true, "Customer and related records deleted successfully"));
                }
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần
                await _logService.LogDeletionAsync(new { Id = id }, $"Failed to delete customer. Exception: {ex.Message}", DateTime.UtcNow);
                return StatusCode(500, new GeneralReponse(false, $"An error occurred: {ex.Message}"));
            }
        }

        [HttpPost("send-mail/{id}")]
        public async Task<IActionResult> SendMail(int id)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerByIdAsync(id);
                if (customer == null) return NotFound("Customer not found.");

                var token = _tokenService.GenerateEmailTokenForCustomer(customer);

                SaveTokenToDatabase(customer.Id, token);

                await _mailRepository.SendMailTestimonialsAsync(customer.Email, "Send Feedback", token);
                return Ok("Success.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private void SaveTokenToDatabase(int customerId, string token)
        {
            var tokenRecord = new EmailTokenDto
            {
                ClientId = customerId,
                Token = token,
                ExpirationDate = DateTime.UtcNow.AddHours(1),
                IsUsed = false
            };
        }
    }
}

