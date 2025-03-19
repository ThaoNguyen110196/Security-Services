using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Entitie.Employee;

namespace Domain.Entities.Entitie.Service
{
    public class JobPosition
    {
       
        public int Id { get; set; }
        public string? JobRequirements { get; set; }
        public string? JobDescription { get; set; }
        public DateTime? ApplicationDeadline { get; set; }

        public string? PositionName { get; set; }
        public string? Status { get; set; }
    }
}
