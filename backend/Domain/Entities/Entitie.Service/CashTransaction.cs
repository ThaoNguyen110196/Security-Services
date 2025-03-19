
using System.Text.Json.Serialization;
using Domain.Entities.Entitie.Employee;


namespace Domain.Entities.Entitie.Service
{
    public class CashTransaction
    {
        public int Id { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string? Status { get; set; }
        public int? EmployeeID { get; set; }


        // Khóa ngoại đến CashService
        public int? CashServiceID { get; set; }
        public CashService? CashService { get; set; }
    }
}
