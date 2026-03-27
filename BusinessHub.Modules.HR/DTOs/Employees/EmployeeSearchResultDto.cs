using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.HR.DTOs
{
    public class EmployeeSearchResultDto
    {
        public int EmployeeID { get; set; }
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public DateTime HireDate { get; set; }
        public string EmployeeLogin { get; set; }
        public bool IsActive { get; set; }

        public EmployeeSearchResultDto(int employeeID, int personID, string firstName, string lastName,
            string jobTitle, DateTime hireDate, string employeeLogin, bool isActive)
        {
            EmployeeID = employeeID;
            PersonID = personID;
            FirstName = firstName;
            LastName = lastName;
            JobTitle = jobTitle;
            HireDate = hireDate;
            EmployeeLogin = employeeLogin;
            IsActive = isActive;
        }
    }
}
