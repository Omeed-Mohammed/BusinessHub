using BusinessHub.Modules.Identity.DTOs.Login;
using BusinessHub.Modules.Identity.Repositories.Login;
using BusinessHub.Modules.Identity.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessHub.Core;
using BusinessHub.Core.Common;

namespace BusinessHub.Modules.Identity.Services.Login
{
    public class LoginServices
    {
        public static bool LoginRequest(LoginRequestDto request)
        {
            // Step 1: Check if a User with the provided Username exists in the database.
            // The Username is used as the unique login identifier.
            UserAuthDto userAuth = LoginRepository.GetUserByInfoUsername(request.Username);


            // If no User is found with the given email,
            // return 401 Unauthorized without revealing which field was wrong.
            if (userAuth == null)
                return false;


            if (!userAuth.IsActive)
            {
                return false;
            }

            // Step 2: Verify the provided password against the stored password hash.
            // BCrypt handles hashing and salt internally.
            // If the password does not match, return 401 Unauthorized.

            bool isValidPassword =
               BCrypt.Net.BCrypt.Verify(request.Password, userAuth.PasswordHash);

            // If the password does not match the stored hash,
            // return 401 Unauthorized.
            if (!isValidPassword)
                return false;

            
            CurrentUser.Username = request.Username;

            return true;

        }


    }
}
