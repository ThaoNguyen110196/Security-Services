using System.Text.Json.Serialization;
using Domain.Entities.Entitie.Service;

namespace Domain.Entities.Entitie.Employee
{
    public class Customer : BaseEntity
    {
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Image { get; set;}
        public ICollection<EmployeeSupport>? EmployeeSupports { get; set; }
        public ICollection<ServiceRequest>? ServiceRequests { get; set; }

        [JsonIgnore]
        public ICollection<Testimonial> Testimonials { get; set; } = new List<Testimonial>();
    }
}