





using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Entitie.Employee
{
    public class Vacation 
    {
        public int Id { get; set; }
  
        public DateTime StartDtae { get; set; }
       
        public int NumberOfDays { get;set; }
     
        public DateTime EndDate  =>  StartDtae.AddDays(NumberOfDays);
        public VacationType? VacationType { get; set; }

        public int? VacationTypeId { get; set; }



       
     
        public Employee? Employee { get; set; }
       
        public int EmployeeId { get; set; }

    }
}