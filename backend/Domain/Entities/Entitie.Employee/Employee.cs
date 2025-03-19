

using System.Text.Json.Serialization;
using Domain.Entities.Entitie.Service;
using Infrastruture.Data;

namespace Domain.Entities.Entitie.Employee
{
    public class Employee : BaseEntity
    {
       
        public string? CivilId { get;set; } = string.Empty;
        public string? Gender {  get; set; }
        public DateTime? Birthday { get; set; }
      
        public string?FileName { get;set; } =string.Empty;
    
        public string? JobName { get; set;} = string.Empty;
        public string? Address {  get; set; } = string.Empty;
        public string? PhoneNumber {  get; set; } = string.Empty;
        public string? Photo { get; set; } = string.Empty;


        public string? Other {  get; set; } = string.Empty;

        public int? EducationId { get; set; }
        public int? PositionId { get; set; }
       
        [JsonIgnore]
        public Education? Education { get; set; }
        [JsonIgnore]
        public Position? Position { get; set; }
        [JsonIgnore]
        public Branch? Branch { get; set; }

        public int? BranchId { get; set; }
        public int? DepartmentId { get; set; }

        [JsonIgnore]
        public Departnent? Departnent { get; set; }

        //contry / province /district

        [JsonIgnore]
        public Country? Country { get; set; }
        public int? CountryId {  get; set; }
        [JsonIgnore]
        public Province? Province { get; set; }
        public string? ProvinceId { get; set; }
        [JsonIgnore]
        public District? District { get; set; }
        public string? DistrictId { get;set;}
        // phân quyền // 
        public bool IsDirector { get; set; }
        public bool IsHeadOfDepartment { get; set; }
        public int? ManagerId { get; set; }
        [JsonIgnore]
        public Manager? Manager { get; set; }
        public ICollection<EmployeeSupport>? EmployeeSupports { get; set; }
    }
}
