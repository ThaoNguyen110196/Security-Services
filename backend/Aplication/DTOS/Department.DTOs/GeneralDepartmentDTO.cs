

namespace Aplication.DTOS.Department.DTOs
{
    public class GeneralDepartmentDTO :BaseDepartment
    {
    }
    public class DepartmentDto: BaseDepartment
    {
      
        public GeneralDepartmentDTO? GeneralDepartment { get; set; }
    }

    public class ManagerDto: BaseDepartment
    {
        
    }

    public class BranchDto: BaseDepartment
    {
      
        public DepartmentDto? Department { get; set; }
        public ManagerDto? Manager { get; set; }
        public BranchDto? Branch { get; set;}
        public GeneralDepartmentDTO? GeneralDepartment { get; set; }
    }
}
