using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Entitie.Employee;

namespace Domain.Entities.Entitie.Service
{
    public class ServiceRequestApproval: BaseEntity
    {
        public int? ServiceRequestID { get; set; }
        public string? ApprovalStatus { get; set; }
        public DateTime? ApprovalDate { get; set; }

        public ServiceRequest? ServiceRequest { get; set; }
    }
}
