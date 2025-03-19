using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities.Entitie.Employee
{
    public class Testimonial
    {
        [Key]
        public int Id { get; set; }

        public string Star { get; set; } = default!;

        [DataType(DataType.MultilineText)]
        public string Desc { get; set; } = default!;

        public int CustomerId { get; set; }

        [JsonIgnore]
        public Customer? Customer { get; set; }
    }
}
