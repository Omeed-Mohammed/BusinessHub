using BusinessHub.Modules.Identity.DTOs.Users;
using BusinessHub.Modules.Identity.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.Identity.Services.Users
{
    public class UserServices
    {
        public static List<UserDto> GetAllUsers(bool? isActive = null)
        {
            return UserRepository.GetAllUsers(isActive);
        }

        public static UserDto GetUserByID(int userID)
        {
            if (userID <= 0)
                throw new ArgumentException("Invalid userID");

            return UserRepository.GetUserByID(userID);
        }

        public static UserDto GetUserByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("username required");

            return UserRepository.GetUserByUsername(username);
        }

        public static bool DeactivateUser(int userID, string currentUser)
        {
            if (userID <= 0)
                throw new ArgumentException("Invalid userID");

            return UserRepository.DeactivateUser(userID, currentUser);
        }

        public static bool ReactivateUser(int userID, string currentUser)
        {
            if (userID <= 0)
                throw new ArgumentException("Invalid userID");

            return UserRepository.ReactivateUser(userID, currentUser);
        }

        public static int AddUser(CreateUserRequestDto request , string currentUser)
        {
            if (request == null)
                throw new ArgumentException("Invalid data");

            if (string.IsNullOrWhiteSpace(request.Username))
                throw new ArgumentException("Username required");

            // Hash the plain-text password using BCrypt before storing it.
            // BCrypt automatically generates and embeds a salt, making the stored value secure.
            // This ensures we never store raw passwords in the database and can safely verify them later using BCrypt.Verify().

            request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);

            return UserRepository.AddUser(request, currentUser);
        }

        public static bool ChangePassword(int userID, string NewPassword, string currentUser)
        {
            if (userID <= 0)
                throw new ArgumentException("Invalid userID");

            if (string.IsNullOrWhiteSpace(NewPassword))
                throw new ArgumentException("NewPassword required");

            // Hash the plain-text password using BCrypt before storing it.
            // BCrypt automatically generates and embeds a salt, making the stored value secure.
            // This ensures we never store raw passwords in the database and can safely verify them later using BCrypt.Verify().

            NewPassword = BCrypt.Net.BCrypt.HashPassword(NewPassword);

            return UserRepository.ChangePassword(userID, NewPassword, currentUser);
        }

    }
}
