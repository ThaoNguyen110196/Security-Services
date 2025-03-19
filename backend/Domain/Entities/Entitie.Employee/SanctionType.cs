using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Entitie.Employee
{
    public class SanctionType :BaseEntity
    {
        public List<Sanction>? Sanctions { get; set; }
    }
}
