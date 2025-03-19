
using Domain.Entities.Entitie.Employee;

namespace Domain.Entities.Entitie.Service
{
    public class TrainingProgram:BaseEntity
    {
        public string? Description { get; set; }
        public string? Objectives { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Instructor { get; set; }
    }
}
