using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Entitie.Employee;

namespace Domain.Entities.Entitie.Service
{
    public class Interview: BaseEntity
    {
        public int? CandidateID { get; set; }
        public DateTime InterviewDate { get; set; }
        public string? InterviewLocation { get; set; }
        public string? InterviewResult { get; set; }
        public string? Comments { get; set; }

        public Candidate? Candidate { get; set; }
    }
}
