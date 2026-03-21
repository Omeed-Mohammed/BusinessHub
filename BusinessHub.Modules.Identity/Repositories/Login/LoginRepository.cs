using BusinessHub.Modules.Identity.DTOs.Login;
using BusinessHub.Modules.Identity.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.Identity.Repositories.Login
{
    public class LoginRepository
    {
        private static readonly string _cs =
            ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static UserAuthDto GetUserByInfoUsername(string username)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_User_Login", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = username;

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    int userIDIndex = reader.GetOrdinal("UserID");
                    int PasswordHashIndex = reader.GetOrdinal("PasswordHash");
                    int isActiveIndex = reader.GetOrdinal("IsActive");


                    if (reader.Read())
                    {
                        return new UserAuthDto(
                            reader.GetInt32(userIDIndex),
                            reader.GetString(PasswordHashIndex),
                            reader.GetBoolean(isActiveIndex)
                        );
                    }
                }
            }

            return null;
        }
    }
}
