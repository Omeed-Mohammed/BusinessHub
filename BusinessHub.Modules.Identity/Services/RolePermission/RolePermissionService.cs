using BusinessHub.Modules.Identity.DTOs.RolePermission;
using BusinessHub.Modules.Identity.Repositories.RolePermission;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.Identity.Services.RolePermission
{
    public class RolePermissionService
    {
        public static int AddRolePermission(RolePermissionDto RolePermission, string currentUser)
        {
            if (RolePermission == null)
                throw new ArgumentException("Invalid data");

            return RolePermissionRepository.AddRolePermission(RolePermission, currentUser);
        }

        public static List<PermissionSummaryDto> GetPermissionsByRoleID(int roleID)
        {
            if (roleID <= 0)
                throw new ArgumentException("Invalid roleID");

            return RolePermissionRepository.GetPermissionsByRoleID(roleID);
        }

        public static List<RoleSummaryDto> GetRolesByPermissionID(int permissionID)
        {
            if (permissionID <= 0)
                throw new ArgumentException("Invalid permissionID");

            return RolePermissionRepository.GetRolesByPermissionID(permissionID);
        }

        public static bool RemoveRolePermission(int roleID, int permissionID, string currentUser)
        {
            if (roleID <= 0)
                throw new ArgumentException("Invalid roleID");

            if (permissionID <= 0)
                throw new ArgumentException("Invalid permissionID");

            return RolePermissionRepository.RemoveRolePermission(roleID, permissionID, currentUser);
        }
    }
}
