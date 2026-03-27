using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.HR.DTOs
{
    public class EmployeeDepartmentDto
    {
        #region Fields

        public int EmployeeDepartmentID { get; set; }

        public int EmployeeID { get; set; }

        public int DepartmentID { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }
        public string DepartmentName { get; set; }

        #endregion

        #region Constructors

        public EmployeeDepartmentDto(
            int employeeDepartmentID,
            int employeeID,
            int departmentID,
            DateTime startDate,
            DateTime? endDate,
            DateTime createdAt,
            string createdBy,
            string departmentName)
        {
            EmployeeDepartmentID = employeeDepartmentID;
            EmployeeID = employeeID;
            DepartmentID = departmentID;
            StartDate = startDate;
            EndDate = endDate;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            DepartmentName = departmentName;
        }



        #endregion
    }
}
