using System.ComponentModel.DataAnnotations;

namespace Employee_MVC.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Salary { get; set; }

        // Foreign Key
        public int DepartmentId { get; set; }

        // Navigation Property
        public Department Department { get; set; }
    }
}
