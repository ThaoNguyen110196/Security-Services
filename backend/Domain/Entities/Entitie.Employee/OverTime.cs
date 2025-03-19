using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Entitie.Employee
{
    public class OverTime 
    {
        public int Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public int NumberOfDay => (StartDate - EndDate).Days;

        //many to  one relationship with vacation type
        public OvertimeType? OvertimeType { get; set; }
        [Required]
        public int OvertimTypeId { get; set; }

        
        public Employee? Employee { get; set; }

        [Required]
        public int? EmployeeId { get; set; }
        // Many to one relationship with Manage (người duyệt giờ làm thêm)
        public Manager? ApprovedBy { get; set; }

        public int? ApprovedById { get; set; } // Người duyệt giờ làm thêm

        public DateTime? ApprovalDate { get; set; } // Ngày duyệt giờ làm thêm

        public string? Remarks { get; set; } // Ghi chú bổ sung
    }
}
