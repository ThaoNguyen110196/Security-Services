using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.Entitie.Employee;

namespace Aplication.DTOS
{
    public class Register :AccountBase
    {
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
   
        public string? Fullname { get; set; }
       

        [DataType(DataType.Password)]
        [Required]
        [Compare(nameof(Password))]
        public string? ConfirmPassword { get; set; }
      

        [Required]
        public int EmployeeId { get; set; }
    }
}
