using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.Identity.DTOs.UserRole
{
    public class UserRoleRoleDto
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }

        public UserRoleRoleDto(int userID, int roleID, string roleName)
        {
            UserID = userID;
            RoleID = roleID;
            RoleName = roleName;
        }
    }
}
