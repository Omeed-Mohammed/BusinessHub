using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.HR.DTOs
{
    public class CreateEmployeeRequestDto
    {
        public int PersonID { get; set; }
        public string JobTitle { get; set; }
        public DateTime HireDate { get; set; }
        public string EmployeeLogin { get; set; }
        public string CreatedBy { get; set; }

        public CreateEmployeeRequestDto(int personID, string jobTitle, DateTime hireDate, 
            string employeeLogin, string createdBy)
        {
            PersonID = personID;
            JobTitle = jobTitle;
            HireDate = hireDate;
            EmployeeLogin = employeeLogin;
            CreatedBy = createdBy;
        }
    }
}
