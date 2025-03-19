namespace Domain.Entities.Entitie.Employee
{
    public class Branch : BaseEntity
    {
        public string Email { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string ContactNumber { get; set; } = default!;
    }
}
