using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.Departments.DTOs.Employees
{
    public class EmployeeDto
    {
        public int EmployeeID { get; set; }
        public int PersonID { get; set; }
        public string JobTitle { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string EmployeeLogin { get; set; }

        public EmployeeDto(int employeeID, int personID, string jobTitle, DateTime hireDate,
            bool isActive, DateTime createdAt, string createdBy,
            DateTime? updatedAt, string updatedBy, string employeeLogin)
        {
            EmployeeID = employeeID;
            PersonID = personID;
            JobTitle = jobTitle;
            HireDate = hireDate;
            IsActive = isActive;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            UpdatedAt = updatedAt;
            UpdatedBy = updatedBy;
            EmployeeLogin = employeeLogin;
        }
    }
}
