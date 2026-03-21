using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.Identity.DTOs.RolePermission
{
    public class RoleSummaryDto
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }

        public RoleSummaryDto(int roleID, string roleName, string description)
        {
            RoleID = roleID;
            RoleName = roleName;
            Description = description;
        }
    }
}
