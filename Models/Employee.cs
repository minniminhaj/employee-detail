using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDetail.Models
{
    public class Employee
    {
        [Required]
        public string EmployeeName { get; set; }
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$",ErrorMessage ="Please enter valid email")]
        public string Email { get; set; }
        [Required]
        public Dept? Department { get; set; }

        public string PhotoPath { get; set; }
    }
}
