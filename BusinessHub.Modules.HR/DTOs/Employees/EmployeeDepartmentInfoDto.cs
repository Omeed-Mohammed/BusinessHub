using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.HR.DTOs
{
    public class EmployeeDepartmentInfoDto
    {
        #region Properties

        public int EmployeeDepartmentID { get; set; }
        public int EmployeeID { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        #endregion

        #region Constructors

        public EmployeeDepartmentInfoDto() { }

        public EmployeeDepartmentInfoDto(
            int employeeDepartmentID,
            int employeeID,
            int departmentID,
            string departmentName,
            DateTime startDate,
            DateTime? endDate,
            DateTime createdAt,
            string createdBy)
        {
            EmployeeDepartmentID = employeeDepartmentID;
            EmployeeID = employeeID;
            DepartmentID = departmentID;
            DepartmentName = departmentName;
            StartDate = startDate;
            EndDate = endDate;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
        }

        #endregion
    }
}
