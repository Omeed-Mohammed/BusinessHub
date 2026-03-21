using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.Identity.DTOs.UserRole
{
    /// <summary>
    /// DTO used to represent roles assigned to a specific user.
    /// 
    /// This object is returned when retrieving all roles belonging to a user
    /// (e.g., SP_UserRole_GetByUserID).
    /// 
    /// It was created because the stored procedure returns RoleName from the Roles table
    /// in addition to UserID and RoleID, which are not sufficient alone to display role
    /// information in the application.
    /// </summary>
    /// 
    public class UserRoleDto
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }

        public UserRoleDto(int userID, int roleID)
        {
            UserID = userID;
            RoleID = roleID;
        }
    }
}
