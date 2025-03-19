namespace Domain.Entities.Entitie.Employee
{
    public class WorkType : BaseEntity
    {
        public float Coefficient { get; set; }
        public List<Attendance>? Attendances { get; set; }
    }
}