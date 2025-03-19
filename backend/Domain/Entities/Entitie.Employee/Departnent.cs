using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities.Entitie.Employee
{
    public class Departnent : BaseEntity
    {
        public int? GeneralDepartmentId { get; set; }

        [JsonIgnore]
        public GeneralDepartment? GeneralDepartment { get; set; }
        public List<Employee>? Employees { get; set; }

    }
}
