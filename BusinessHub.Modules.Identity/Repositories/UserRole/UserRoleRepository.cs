using BusinessHub.Modules.Identity.DTOs.UserRole;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.Identity.Repositories.UserRole
{
    public class UserRoleRepository
    {
        private static readonly string _cs =
            ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static bool AddUserRole(int userID, int roleID, string currentUser)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_UserRole_Add", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                command.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleID;
                command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        public static bool RemoveUserRole(int userID, int roleID, string currentUser)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_UserRole_Remove", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                command.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleID;
                command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }


        public static List<UserRoleUserDto> GetUsersByRoleID(int roleID)
        {
            var users = new List<UserRoleUserDto>();

            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_UserRole_GetByRoleID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleID;

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    int userIDIndex = reader.GetOrdinal("UserID");
                    int usernameIndex = reader.GetOrdinal("Username");
                    int roleIDIndex = reader.GetOrdinal("RoleID");

                    while (reader.Read())
                    {
                        users.Add(new UserRoleUserDto(
                            reader.GetInt32(userIDIndex),
                            reader.GetString(usernameIndex),
                            reader.GetInt32(roleIDIndex)
                        ));
                    }
                }
            }

            return users;
        }

        public static List<UserRoleRoleDto> GetRolesByUserID(int userID)
        {
            var roles = new List<UserRoleRoleDto>();

            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_UserRole_GetByUserID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    int userIDIndex = reader.GetOrdinal("UserID");
                    int roleIDIndex = reader.GetOrdinal("RoleID");
                    int roleNameIndex = reader.GetOrdinal("RoleName");

                    while (reader.Read())
                    {
                        roles.Add(new UserRoleRoleDto(
                            reader.GetInt32(userIDIndex),
                            reader.GetInt32(roleIDIndex),
                            reader.GetString(roleNameIndex)
                        ));
                    }
                }
            }

            return roles;
        }
    }
}
