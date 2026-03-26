using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.Departments.DTOs.Employees
{
    public class EmployeeDepartmentDto
    {
        public int EmployeeDepartmentID { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public EmployeeDepartmentDto(int employeeDepartmentID, int departmentID, string departmentName,
            DateTime startDate, DateTime? endDate, DateTime createdAt, string createdBy)
        {
            EmployeeDepartmentID = employeeDepartmentID;
            DepartmentID = departmentID;
            DepartmentName = departmentName;
            StartDate = startDate;
            EndDate = endDate;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
        }
    }
}
