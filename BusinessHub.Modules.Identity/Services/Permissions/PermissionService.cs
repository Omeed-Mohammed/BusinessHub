using BusinessHub.Modules.Identity.DTOs.Permissions;
using BusinessHub.Modules.Identity.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.Identity.Services.Permissions
{
    public class PermissionService
    {
        public static int AddPermission(PermissionDto permission , string currentUser)
        {
            if (permission == null)
                throw new ArgumentException("Invalid data");

            if (string.IsNullOrWhiteSpace(permission.PermissionName))
                throw new ArgumentException("PermissionName required");

            return PermissionRepository.AddPermission(permission, currentUser);
        }

        public static bool UpdatePermission(PermissionDto permission, string currentUser)
        {
            if (permission == null || permission.PermissionID <= 0)
                throw new ArgumentException("Invalid data");

            if (string.IsNullOrWhiteSpace(permission.PermissionName))
                throw new ArgumentException("PermissionName required");

            return PermissionRepository.UpdatePermission(permission, currentUser);
        }

        public static List<PermissionDto> GetAllPermissions()
        {
            return PermissionRepository.GetAllPermissions();
        }

        public static PermissionDto GetPermissionByID(int permissionID)
        {
            if (permissionID <= 0)
                throw new ArgumentException("Invalid permissionID");

            return PermissionRepository.GetPermissionByID(permissionID);
        }

        public static bool DeactivatePermission(int permissionID, string currentUser)
        {
            if (permissionID <= 0)
                throw new ArgumentException("Invalid permissionID");
            
            return PermissionRepository.DeactivatePermission(permissionID, currentUser);
        }

        public static bool ReactivatePermission(int permissionID, string currentUser)
        {
            if (permissionID <= 0)
                throw new ArgumentException("Invalid permissionID");

            return PermissionRepository.ReactivatePermission(permissionID, currentUser);
        }

    }
}
