

namespace Domain.Entities.Entitie.Employee
{
    public class Attendance
    {
      
            public int Id { get; set; }
            public int? Year { get; set; }
            public int? Month { get; set; }
            public int? Day { get; set; }
            public int? ReportedHours { get; set; }
            public int? MinutesIn { get; set; }
            public int? MinutesOut { get; set; }
            public int? HoursOut { get; set; }

             public Employee? Employee { get; set; }
            public int? EmployeeId { get; set; }

           public  WorkType? WorkType {  get; set; }
            public int? WorkTypeId { get; set; }
        
    }
}
