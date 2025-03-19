using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Entitie.Employee;

namespace Domain.Entities.Entitie.Service
{
    public class Candidate : BaseEntity
    {
        public string? Email { get; set; }
        public string? CvFilePath {  get; set; }
        public string? Phone { get; set; }
        public string? Status { get; set; }
        public List<Interview>? Interviews { get; set; } // Một ứng viên có thể có nhiều buổi phỏng vấn
    }
}
