using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.Identity.DTOs.Login
{
    public class UserAuthDto
    {
        public int UserID { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }

        public UserAuthDto(int userID,
            string passwordHash, bool isActive)
        {
            UserID = userID;
            PasswordHash = passwordHash;
            IsActive = isActive;
        }
    }
}
