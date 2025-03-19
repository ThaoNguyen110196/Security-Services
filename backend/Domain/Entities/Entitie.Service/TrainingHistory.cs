
using Domain.Entities.Entitie.Employee;

namespace Domain.Entities.Entitie.Service
{
    public class TrainingHistory:BaseEntity
    {
        public int? ProgramID { get; set; }
        public int? EmployeeID { get; set; }
        public string? CompletionStatus { get; set; }
        public DateTime? CompletionDate { get; set; }

        public TrainingProgram? TrainingProgram { get; set; }
    }
}
