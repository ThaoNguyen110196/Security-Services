using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Entitie.Employee;

namespace Domain.Entities.Entitie.Service
{
    public class ServiceRequest: BaseEntity
    {
        public int? CustomerID { get; set; }
        public string? ServiceType { get; set; }
        public string? RequestDetails { get; set; }
        public string? Status { get; set; }

        public Customer? Customer { get; set; }
    }
}
