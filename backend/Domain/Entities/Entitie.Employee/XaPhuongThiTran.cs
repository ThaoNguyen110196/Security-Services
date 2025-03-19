using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Entitie.Employee
{
    public class XaPhuongThiTran
    {
        [Key]
        [MaxLength(5)]
        public string Xaid { get; set; }

        [Required]
        [MaxLength(100)]
        public string NameXaPhuong { get; set; }

        [Required]
        [MaxLength(30)]
        public string Type { get; set; }

        [ForeignKey("QuanHuyen")]
        public string Maqh { get; set; }

        // Navigation property
        public QuanHuyen QuanHuyen { get; set; }
        public List<Employee> Employees { get; set; }

    }
}