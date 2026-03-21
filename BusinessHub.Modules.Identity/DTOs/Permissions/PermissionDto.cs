using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.Identity.DTOs.Permissions
{
    public class PermissionDto
    {
        public int PermissionID { get; set; }
        public string PermissionName { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

      
        public PermissionDto(int permissionID, string permissionName, 
            string description, string createdBy, DateTime createdAt, bool isActive)
        {
            PermissionID = permissionID;
            PermissionName = permissionName;
            Description = description;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
            IsActive = isActive;
        }
    }
}
