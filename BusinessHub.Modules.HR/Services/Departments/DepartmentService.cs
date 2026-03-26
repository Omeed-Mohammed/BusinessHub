using BusinessHub.Core.Common;
using BusinessHub.Modules.HR.DTOs.Departments;
using BusinessHub.Modules.HR.Repositories.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.HR.Services.Departments
{
    public class DepartmentService
    {
        public static int AddDepartment(CreateDepartmentRequestDto departmentRequest)
        {
            if (departmentRequest == null)
                throw new ArgumentException("Invalid data");

            if (string.IsNullOrWhiteSpace(departmentRequest.DepartmentName))
                throw new ArgumentException("Department Name required");

            var departmentDto = new DepartmentDto(0, departmentRequest.DepartmentName,
                false, default, CurrentUser.Username, null, null);


            return DepartmentRepository.AddDepartment(departmentDto);
        }


        public static bool UpdateDepartment(UpdateDepartmentRequestDto department)
        {
            if (department == null || department.DepartmentID <= 0)
                throw new ArgumentException("Invalid data");

            var departmentDto = new DepartmentDto(department.DepartmentID, department.DepartmentName,
               department.IsActive, default, null, null, CurrentUser.Username);

            return DepartmentRepository.UpdateDepartment(departmentDto);
        }


        public static List<DepartmentDto> GetAllDepartments()
        {
            return DepartmentRepository.GetAllDepartments();
        }

        public static DepartmentDto GetDepartmentByID(int DepartmentID)
        {
            if (DepartmentID <= 0)
                throw new ArgumentException("Invalid DepartmentID");

            return DepartmentRepository.GetDepartmentByID(DepartmentID);
        }

        public static DepartmentDto GetDepartmentByName(string DepartmentName)
        {
            if (string.IsNullOrWhiteSpace(DepartmentName))
                throw new ArgumentException("Department Name required");

            return DepartmentRepository.GetDepartmentByName(DepartmentName);
        }
    }
}
