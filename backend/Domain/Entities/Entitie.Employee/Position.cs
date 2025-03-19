using Domain.Entities.Entitie.Employee;

namespace Infrastruture.Data
{
    public class Position : BaseEntity
    {
        public ICollection<Employee>? Employees { get; set; }
    }
}