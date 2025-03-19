using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Entitie.Employee
{
    public class Sanction 
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Punishment {  get; set; } = string.Empty;
        [Required]
        public DateTime PunishmentDate { get; set; }

        public Employee?Employee { get; set; }

        public int EmployeeId { get; set; }
        //MANY TO ONE RELATIONSHIP  WITH VAACATION TYPE
        public int SanctionId { get; set; }
        public SanctionType? SanctionType { get; set; }
    }
}
