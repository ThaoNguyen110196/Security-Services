using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Entitie.Employee
{
    public class Education : BaseEntity
    {
        public ICollection<Employee>? Employees { get; set; }
    }
}
