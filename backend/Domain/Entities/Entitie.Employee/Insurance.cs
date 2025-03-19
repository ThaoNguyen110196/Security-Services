using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Entitie.Employee
{
    public class Insurance
    {
        public int Id { get; set; }
        public string InsuranceNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public string IssuePlace { get; set; }
        public string HealthCheckPlace { get; set; }

        public Employee? Employee { get; set; }
        public int EmployeeId { get; set; }
    }
}
