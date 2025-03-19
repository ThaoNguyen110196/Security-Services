using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Entitie.Employee
{
    public class City 
    {
        [Key]
        [MaxLength(5)]
        public string Matp { get; set; }

        [Required]
        [MaxLength(100)]
        public string NameCity { get; set; }

        [Required]
        [MaxLength(30)]
        public string Type { get; set; }

        // Navigation property
        public ICollection<QuanHuyen> QuanHuyens { get; set; }
     
    }
}
