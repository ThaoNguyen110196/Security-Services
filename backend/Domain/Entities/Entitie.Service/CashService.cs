using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Entitie.Employee;

namespace Domain.Entities.Entitie.Service
{
    public class CashService:BaseEntity
    {
        public string? ServiceType { get; set; }
        public string? Scope { get; set; }
        public decimal? Price { get; set; }
        public string? Conditions { get; set; }
        public ICollection<EmployeeSupport>? EmployeeSupports { get; set; }
        public ICollection<CashTransaction> CashTransactions { get; set; } = new List<CashTransaction>();
    }
}
