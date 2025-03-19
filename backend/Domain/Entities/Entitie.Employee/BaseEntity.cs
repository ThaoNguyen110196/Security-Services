using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Entitie.Employee
{
    public class BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}
