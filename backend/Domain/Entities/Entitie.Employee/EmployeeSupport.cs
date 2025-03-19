using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Entitie.Service;

namespace Domain.Entities.Entitie.Employee
{
    public class EmployeeSupport
    {
        public int Id {  get; set; }
        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public int? CashServiceId { get; set; }
        public CashService? CashService { get; set; }

        public int? ServiceId { get; set; }
        public Service? Service { get; set; }
        public bool IsDeleted { get; set; }=false;
    }
}
