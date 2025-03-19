namespace Aplication.DTOS.Employee.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CivilId { get; set; }
        public string? Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string? FileName { get; set; }
        public string? JobName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Photo { get; set; }
        public string? Other { get; set; }
        public int EducationId { get; set; }
        public int? BranchId { get; set; }
        public int CountryId { get; set; }
        public string? ProvinceId { get; set; }
        public string? DistrictId { get; set; }
        public bool IsDirector { get; set; }
        public bool IsHeadOfDepartment { get; set; }
        public int? ManagerId { get; set; }
        public int? PositionId { get; set; }
    }
}
