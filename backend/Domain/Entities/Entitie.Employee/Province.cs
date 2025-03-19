using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities.Entitie.Employee
{
    public class Province
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string NameCity { get; set; }

        [Required]
        [MaxLength(30)]
        public string Type { get; set; }

        [Required]
        public int CountryId {  get; set; }
        [JsonIgnore] // Bỏ qua thuộc tính này khi serializing
        public Country? Country { get; set; }
        // Navigation property
        public List<District>? District { get; set; }
        public List<Employee>? Employees { get; set; }

    }
}