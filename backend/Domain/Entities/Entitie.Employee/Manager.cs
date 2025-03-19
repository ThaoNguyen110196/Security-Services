using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities.Entitie.Employee
{
    public class Manager
    {
        [Key]
        public int Id { get; set; }
         
        public string Name { get; set; }
        public int EmployeeId { get; set; }
        
        [JsonIgnore]
        public ICollection<Employee>? Employees { get; set; }
    }
}
