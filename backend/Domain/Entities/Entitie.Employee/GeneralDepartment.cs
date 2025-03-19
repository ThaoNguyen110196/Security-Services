using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities.Entitie.Employee
{
    public class GeneralDepartment : BaseEntity
    {
        [JsonIgnore]
        public List<Departnent>? Departnents { get; set; }
    }
}
