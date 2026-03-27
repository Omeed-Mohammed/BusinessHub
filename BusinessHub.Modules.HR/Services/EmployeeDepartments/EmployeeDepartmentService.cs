using BusinessHub.Core.Common;
using BusinessHub.Modules.HR.DTOs;
using BusinessHub.Modules.HR.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.HR.Services
{
    public class EmployeeDepartmentService
    {
        public static int AssignEmployeeDepartment(int employeeID, int departmentID)
        {
            if (employeeID <= 0 || departmentID <= 0)
                throw new ArgumentException("Invalid data");

            var dto = new EmployeeDepartmentDto(
                 0,                      // EmployeeDepartmentID
                 employeeID,
                 departmentID,
                 DateTime.Now,           // StartDate
                 null,                   // EndDate
                 DateTime.Now,           // CreatedAt
                 CurrentUser.Username,   // CreatedBy
                 string.Empty            // DepartmentName (غير مهم هنا)
             );

            return EmployeeDepartmentRepository.AssignEmployeeDepartment(dto);
        }


        public static List<EmployeeDepartmentDto> GetByEmployeeID(int employeeID, bool? isActive)
        {
            if (employeeID <= 0)
                throw new ArgumentException("Invalid EmployeeID");

            return EmployeeDepartmentRepository.GetByEmployeeID(
                employeeID,
                isActive,
                CurrentUser.Username
            );
        }
    }
}
