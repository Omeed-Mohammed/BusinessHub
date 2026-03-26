using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.HR.DTOs.Departments
{
    public class CreateDepartmentRequestDto
    {
        public string DepartmentName {  get; set; }
        
        public CreateDepartmentRequestDto (string departmentName)
        {
            DepartmentName = departmentName;
        }
    }
}
