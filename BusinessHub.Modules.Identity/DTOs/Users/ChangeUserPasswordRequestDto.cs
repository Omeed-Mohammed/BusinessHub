using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.Identity.DTOs.Users
{
    public class ChangeUserPasswordRequestDto
    {
        public int UserID { get; set; }

        public string NewPassword { get; set; }

        public ChangeUserPasswordRequestDto(int userID, string newPassword)
        {
            UserID = userID;
            NewPassword = newPassword;
        }
    }
}
