using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.HR.DTOs
{
    public class UpdateEmployeeRequestDto
    {
        public int EmployeeID { get; set; }
        public string JobTitle { get; set; }
        public DateTime HireDate { get; set; }
        public string EmployeeLogin { get; set; }
        public string UpdatedBy { get; set; }

        public UpdateEmployeeRequestDto(int employeeID, string jobTitle, DateTime hireDate, 
            string employeeLogin, string updatedBy)
        {
            EmployeeID = employeeID;
            JobTitle = jobTitle;
            HireDate = hireDate;
            EmployeeLogin = employeeLogin;
            UpdatedBy = updatedBy;
        }
    }
}
