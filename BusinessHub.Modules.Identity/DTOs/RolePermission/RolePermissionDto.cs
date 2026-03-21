using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.Identity.DTOs.RolePermission
{
    public class RolePermissionDto
    {
        public int RoleID { get; set; }
        public int PermissionID { get; set; }

        public RolePermissionDto(int roleID, int permissionID)
        {
            RoleID = roleID;
            PermissionID = permissionID;
        }
    }
}
