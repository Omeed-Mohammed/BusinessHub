using BusinessHub.Core.Common;
using BusinessHub.Modules.Departments.DTOs.Employees;
using BusinessHub.Modules.Departments.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.Departments.Services.Employees
{
    public class EmployeeService
    {
        public static int AddEmployee(CreateEmployeeRequestDto request)
        {
            if (request == null)
                throw new ArgumentException("Invalid data");

            if (request.PersonID <= 0)
                throw new ArgumentException("Invalid PersonID");

            if (string.IsNullOrWhiteSpace(request.JobTitle))
                throw new ArgumentException("JobTitle required");

            if (string.IsNullOrWhiteSpace(request.EmployeeLogin))
                throw new ArgumentException("Login required");

            var dto = new EmployeeDto(
                0,
                request.PersonID,
                request.JobTitle,
                request.HireDate,
                true,
                DateTime.Now,
                CurrentUser.Username,
                null,
                null,
                request.EmployeeLogin
            );

            return EmployeeRepository.AddEmployee(dto);
        }

        public static bool UpdateEmployee(UpdateEmployeeRequestDto request)
        {
            if (request == null)
                throw new ArgumentException("Invalid data");

            if (request.EmployeeID <= 0)
                throw new ArgumentException("Invalid EmployeeID");

            if (string.IsNullOrWhiteSpace(request.JobTitle))
                throw new ArgumentException("JobTitle required");

            var dto = new EmployeeDto(
                request.EmployeeID,
                0, // not used
                request.JobTitle,
                request.HireDate,
                true,
                DateTime.MinValue,
                null,
                null,
                CurrentUser.Username,
                request.EmployeeLogin
            );

            return EmployeeRepository.UpdateEmployee(dto);
        }

        public static bool DeactivateEmployee(int employeeId)
        {
            if (employeeId <= 0)
                throw new ArgumentException("Invalid EmployeeID");

            return EmployeeRepository.DeactivateEmployee(employeeId, CurrentUser.Username);
        }

        public static bool ReactivateEmployee(int employeeId)
        {
            if (employeeId <= 0)
                throw new ArgumentException("Invalid EmployeeID");

            return EmployeeRepository.ReactivateEmployee(employeeId, CurrentUser.Username);
        }

        public static EmployeeDto GetEmployeeById(int employeeId, bool isActive)
        {
            if (employeeId <= 0)
                throw new ArgumentException("Invalid EmployeeID");

            return EmployeeRepository.GetEmployeeById(employeeId, isActive , CurrentUser.Username);
        }

        public static List<EmployeeDto> GetAllEmployees(bool? isActive)
        {
            return EmployeeRepository.GetAllEmployees(isActive);
        }

        public static EmployeeDto GetEmployeeByLogin(string login, bool? isActive)
        {
            if (string.IsNullOrWhiteSpace(login))
                throw new ArgumentException("Login required");

            return EmployeeRepository.GetEmployeeByLogin(login, isActive, CurrentUser.Username);
        }

        public static List<EmployeeSearchResultDto> SearchEmployees(string firstName, string lastName, bool? isActive)
        {
            if (string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("At least one name required");

            return EmployeeRepository.SearchEmployees(firstName, lastName, isActive, CurrentUser.Username);
        }

        public static (EmployeeDto employee, List<EmployeeRoleDto> roles, List<EmployeeDepartmentDto> departments)
             GetEmployeeProfile(int employeeId, bool? isActive)
        {
            if (employeeId <= 0)
                throw new ArgumentException("Invalid EmployeeID");

            return EmployeeRepository.GetEmployeeProfile(employeeId, isActive, CurrentUser.Username);
        }
    }
}
