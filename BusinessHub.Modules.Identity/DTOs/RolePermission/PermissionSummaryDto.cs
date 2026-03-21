using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.Identity.DTOs.RolePermission
{
    public class PermissionSummaryDto
    {
        public int PermissionID { get; set; }
        public string PermissionName { get; set; }
        public string Description { get; set; }

        public PermissionSummaryDto(int permissionID, string permissionName, string description)
        {
            PermissionID = permissionID;
            PermissionName = permissionName;
            Description = description;
        }
    }
}
