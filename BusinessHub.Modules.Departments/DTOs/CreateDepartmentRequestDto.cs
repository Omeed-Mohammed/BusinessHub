using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.Departments.DTOs
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
