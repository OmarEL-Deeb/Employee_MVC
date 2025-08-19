using Microsoft.EntityFrameworkCore;

namespace Employee_MVC.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          
            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = 1, Name = "Markting" },
                new Department { DepartmentId = 2, Name = "HR" },
                new Department { DepartmentId = 3, Name = "PR" }
            );

            // Seed Employees
            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, Name = "Khaled Ali", Salary = 8000, DepartmentId = 1 },
                new Employee { EmployeeId = 2, Name = "Omar Mohamed", Salary = 9000, DepartmentId = 2 },
                new Employee { EmployeeId = 3, Name = "Khaled Mohamed", Salary = 7500, DepartmentId = 3 }
            );
        }
    }
}
