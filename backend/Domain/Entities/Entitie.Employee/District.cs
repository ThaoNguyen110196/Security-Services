using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Domain.Entities.Entitie.Employee
{
    public class District
    {


        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string NameDistrict{ get; set; }

        [Required]
        [MaxLength(30)]
        public string Type { get; set; }

        [ForeignKey("Province")]
        public int ProvinceId { get; set; }

        // Navigation property
         [JsonIgnore]
        public Province? Province { get; set; }

        public List<Employee>? Employees { get; set; }
    }
}
