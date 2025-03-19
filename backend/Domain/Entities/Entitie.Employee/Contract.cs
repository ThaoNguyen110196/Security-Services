using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Entitie.Employee
{
    public class Contract
    {
        public int Id { get; set; }
        public string? ContractNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? SignDate { get; set; }
        public string? Content { get; set; }

        public Employee? Employee { get; set; }
        public int? EmployeeId { get; set; }
        public int? RenewalCount { get; set; }
        public int? Duration { get; set; }
    }
}
