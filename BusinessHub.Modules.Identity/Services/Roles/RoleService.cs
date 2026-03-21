using BusinessHub.Modules.Identity.DTOs.Roles;
using BusinessHub.Modules.Identity.Repositories.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.Identity.Services.Roles
{
    public class RoleService
    {
        public static int AddRole(RoleDto role, string currentUser)
        {
            if (role == null)
                throw new ArgumentException("Invalid data");

            if (string.IsNullOrWhiteSpace(role.RoleName))
                throw new ArgumentException("RoleName required");

            return RoleRepository.AddRole(role, currentUser);
        }

        public static bool UpdateRole(RoleDto role, string currentUser)
        {
            if (role == null || role.RoleID <= 0)
                throw new ArgumentException("Invalid data");

            if (string.IsNullOrWhiteSpace(role.RoleName))
                throw new ArgumentException("RoleName required");

            return RoleRepository.UpdateRole(role, currentUser);
        }

        public static List<RoleDto> GetAllRoles()
        {
            return RoleRepository.GetAllRoles();
        }

        public static RoleDto GetRoleByID(int roleID)
        {
            if (roleID <= 0)
                throw new ArgumentException("Invalid roleID");

            return RoleRepository.GetRoleByID(roleID);
        }

        public static bool DeactivateRole(int roleID, string currentUser)
        {
            if (roleID <= 0)
                throw new ArgumentException("Invalid roleID");
            
            return RoleRepository.DeactivateRole(roleID, currentUser);
        }

        public static bool ReactivateRole(int roleID, string currentUser)
        {
            if (roleID <= 0)
                throw new ArgumentException("Invalid roleID");

            return RoleRepository.ReactivateRole(roleID, currentUser);
        }
    }
}
