
using Domain.Entities.Entitie.Employee;

namespace Domain.Entities.Entitie.Service
{
    public class ServiceSchedule :BaseEntity
    {
        public int? ServiceRequestID { get; set; }
        public int? EmployeeID { get; set; }
      
             
        public DateTime? ScheduledDate { get; set; }
        public string? Location { get; set; }

        public ServiceRequest? ServiceRequest { get; set; }
    }
}
