using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.Departments.DTOs.Employees
{
    public class EmployeeRoleDto
    {
        public int EmployeeRoleID { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime AssignedAt { get; set; }
        public string AssignedBy { get; set; }
        public DateTime? RevokedAt { get; set; }
        public string RevokedBy { get; set; }

        public EmployeeRoleDto(int employeeRoleID, int roleID, string roleName, bool isActive,
            DateTime startDate, DateTime? endDate, DateTime assignedAt, string assignedBy,
            DateTime? revokedAt, string revokedBy)
        {
            EmployeeRoleID = employeeRoleID;
            RoleID = roleID;
            RoleName = roleName;
            IsActive = isActive;
            StartDate = startDate;
            EndDate = endDate;
            AssignedAt = assignedAt;
            AssignedBy = assignedBy;
            RevokedAt = revokedAt;
            RevokedBy = revokedBy;
        }
    }
}
