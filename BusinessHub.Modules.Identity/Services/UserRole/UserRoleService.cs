using BusinessHub.Modules.Identity.DTOs.UserRole;
using BusinessHub.Modules.Identity.Repositories.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.Identity.Services.UserRole
{
    public class UserRoleService
    {
        public static bool AddUserRole(int userID, int roleID, string currentUser)
        {
            if (userID <= 0)
                throw new ArgumentException("Invalid userID");

            if (roleID <= 0)
                throw new ArgumentException("Invalid roleID");

            return UserRoleRepository.AddUserRole(userID, roleID, currentUser);
        }

        public static bool RemoveUserRole(int userID, int roleID, string currentUser)
        {
            if (userID <= 0)
                throw new ArgumentException("Invalid userID");

            if (roleID <= 0)
                throw new ArgumentException("Invalid roleID");

            return UserRoleRepository.RemoveUserRole(userID, roleID, currentUser);
        }

        public static List<UserRoleUserDto> GetUsersByRoleID(int roleID)
        {
            if (roleID <= 0)
                throw new ArgumentException("Invalid roleID");

            return UserRoleRepository.GetUsersByRoleID(roleID);
        }

        public static List<UserRoleRoleDto> GetRolesByUserID(int userID)
        {
            if (userID <= 0)
                throw new ArgumentException("Invalid userID");

            return UserRoleRepository.GetRolesByUserID(userID);
        }
    }
}
