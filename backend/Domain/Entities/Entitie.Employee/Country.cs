using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Entitie.Employee
{
    public class Country:BaseEntity
    {
        public List<Province>? Provinces { get; set; }
        public List<Employee>? Employees { get; set; }
    }
}
