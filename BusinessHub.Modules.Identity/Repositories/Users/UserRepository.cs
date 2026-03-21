using BusinessHub.Modules.Identity.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.Identity.Repositories.Users
{
    
    public class UserRepository
    {
        private static readonly string _cs =
            ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;


        public static List<UserDto> GetAllUsers(bool? isActive = null)
        {
            var users = new List<UserDto>();

            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_User_GetAll", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value =
                isActive.HasValue ? (object)isActive.Value : DBNull.Value;

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    int userIDIndex = reader.GetOrdinal("UserID");
                    int personIDIndex = reader.GetOrdinal("PersonID");
                    int usernameIndex = reader.GetOrdinal("Username");
                    int isActiveIndex = reader.GetOrdinal("IsActive");
                    int createdAtIndex = reader.GetOrdinal("CreatedAt");
                    int createdByIndex = reader.GetOrdinal("CreatedBy");

                    while (reader.Read())
                    {
                        users.Add(new UserDto(
                            reader.GetInt32(userIDIndex),
                            reader.GetInt32(personIDIndex),
                            reader.GetString(usernameIndex),
                            reader.GetBoolean(isActiveIndex),
                            reader.GetDateTime(createdAtIndex),
                            reader.GetString(createdByIndex)
                        ));
                    }
                }
            }

            return users;
        }

        public static UserDto GetUserByID(int userID)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_User_GetByID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    int userIDIndex = reader.GetOrdinal("UserID");
                    int personIDIndex = reader.GetOrdinal("PersonID");
                    int usernameIndex = reader.GetOrdinal("Username");
                    int isActiveIndex = reader.GetOrdinal("IsActive");
                    int createdAtIndex = reader.GetOrdinal("CreatedAt");
                    int createdByIndex = reader.GetOrdinal("CreatedBy");

                    if (reader.Read())
                    {
                        return new UserDto(
                            reader.GetInt32(userIDIndex),
                            reader.GetInt32(personIDIndex),
                            reader.GetString(usernameIndex),
                            reader.GetBoolean(isActiveIndex),
                            reader.GetDateTime(createdAtIndex),
                            reader.GetString(createdByIndex)
                        );
                    }
                }
            }

            return null;
        }

        public static UserDto GetUserByUsername(string username)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_User_GetByUsername", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = username;

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    int userIDIndex = reader.GetOrdinal("UserID");
                    int personIDIndex = reader.GetOrdinal("PersonID");
                    int usernameIndex = reader.GetOrdinal("Username");
                    int isActiveIndex = reader.GetOrdinal("IsActive");
                    int createdAtIndex = reader.GetOrdinal("CreatedAt");
                    int createdByIndex = reader.GetOrdinal("CreatedBy");

                    if (reader.Read())
                    {
                        return new UserDto(
                            reader.GetInt32(userIDIndex),
                            reader.GetInt32(personIDIndex),
                            reader.GetString(usernameIndex),
                            reader.GetBoolean(isActiveIndex),
                            reader.GetDateTime(createdAtIndex),
                            reader.GetString(createdByIndex)
                        );
                    }
                }
            }

            return null;
        }

        public static bool DeactivateUser(int userID, string currentUser)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_User_Deactivate", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        public static bool ReactivateUser(int userID, string currentUser)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_User_Reactivate", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        public static int AddUser(CreateUserRequestDto request , string currentUser)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_User_Add", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@PersonID", SqlDbType.Int).Value = request.PersonID;
                command.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = request.Username;
                command.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 250).Value = request.Password;
                command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

                connection.Open();

                return Convert.ToInt32(command.ExecuteScalar());
            }
        }


        public static bool ChangePassword(int userID, string passwordHash, string currentUser)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_User_ChangePassword", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                command.Parameters.Add("@NewPasswordHash", SqlDbType.NVarChar, 250).Value = passwordHash;
                command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

    }
}
