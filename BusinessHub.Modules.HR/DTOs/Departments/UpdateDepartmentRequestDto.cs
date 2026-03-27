using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.HR.DTOs
{
    public class UpdateDepartmentRequestDto
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public bool IsActive { get; set; }

        public UpdateDepartmentRequestDto(int departmentID , string departmentName , bool isActive) 
        {
            DepartmentID = departmentID;
            DepartmentName = departmentName;
            IsActive = isActive;
        }


    }
}
