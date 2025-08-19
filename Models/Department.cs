using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Employee_MVC.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
