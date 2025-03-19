using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Entitie.Service
{
    public class DeletionLog
    {
        public int Id { get; set; }
        public string? EntityType { get; set; }
        public int? EntityId { get; set; }
        public string? Details { get; set; }
        public DateTime? DeletionDate { get; set; }
    }
}
