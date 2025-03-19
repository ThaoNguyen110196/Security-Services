using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Aplication.DTOS.Service.DTO.ServiceDto;

namespace Aplication.DTOS.Employee.DTOs
{
    public class CustomerEmployeeSupportDto
    {
        public int Id { get; set; } // Assuming this is the CustomerId
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Image { get; set; }

        // Employee and Service IDs
        public List<int>? EmployeeIds { get; set; } // List of Employee IDs
        public List<int>? ServiceIds { get; set; }  // List of Service IDs
        public int? CashServiceId { get; set; }
        public int? ServiceId { get; set; }
    }
}
