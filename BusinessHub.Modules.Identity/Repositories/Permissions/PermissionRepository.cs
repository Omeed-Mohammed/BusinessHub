using BusinessHub.Modules.Identity.DTOs.Permissions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace BusinessHub.Modules.Identity.Repositories
{
    public class PermissionRepository
    {
        private static readonly string _cs = 
            ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static int AddPermission(PermissionDto permission , string currentUser)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_Permission_Add", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@PermissionName", SqlDbType.NVarChar, 100).Value = permission.PermissionName;

                command.Parameters.Add("@Description", SqlDbType.NVarChar, 250).Value =
                    (object)permission.Description ?? DBNull.Value;

                command.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 100).Value =
                    (object)currentUser ?? DBNull.Value;

                connection.Open();
                var result = command.ExecuteScalar();

                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        public static bool UpdatePermission(PermissionDto permission, string currentUser)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_Permission_Update", connection))
            {
                command.CommandType = CommandType.StoredProcedure;


                command.Parameters.Add("@PermissionID", SqlDbType.Int).Value = permission.PermissionID;

                command.Parameters.Add("@PermissionName", SqlDbType.NVarChar, 100).Value = permission.PermissionName;

                command.Parameters.Add("@Description", SqlDbType.NVarChar, 250).Value =
                    (object)permission.Description ?? DBNull.Value;

                command.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 100).Value =
                    (object)currentUser ?? DBNull.Value;


                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }


        public static List<PermissionDto> GetAllPermissions()
        {
            var permissions = new List<PermissionDto>();

            using (SqlConnection conn = new SqlConnection(_cs))
            {


                using (SqlCommand cmd = new SqlCommand("SP_Permission_GetAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int permissionIDIndex = reader.GetOrdinal("PermissionID");

                        int permissionNameIndex = reader.GetOrdinal("PermissionName");
                        int descriptionIndex = reader.GetOrdinal("Description");
                        int createdByIndex = reader.GetOrdinal("CreatedBy");

                        int createdAtIndex = reader.GetOrdinal("CreatedAt");

                        int isActiveIndex = reader.GetOrdinal("IsActive");


                        while (reader.Read())
                        {
                            string description = reader.IsDBNull(descriptionIndex)
                                ? null
                                : reader.GetString(descriptionIndex);

                            string createdBy = reader.IsDBNull(createdByIndex)
                                ? null
                                : reader.GetString(createdByIndex);



                            permissions.Add(new PermissionDto(
                                reader.GetInt32(permissionIDIndex),
                                reader.GetString(permissionNameIndex),
                                description,
                                createdBy,
                                reader.GetDateTime(createdAtIndex),
                                reader.GetBoolean(isActiveIndex)
                            ));
                        }
                    }
                }
            }

            return permissions;
        }


        public static PermissionDto GetPermissionByID(int permissionID)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_Permission_GetByID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@PermissionID", SqlDbType.Int).Value = permissionID;

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    int permissionIDIndex = reader.GetOrdinal("PermissionID");

                    int permissionNameIndex = reader.GetOrdinal("PermissionName");
                    int descriptionIndex = reader.GetOrdinal("Description");
                    int createdByIndex = reader.GetOrdinal("CreatedBy");

                    int createdAtIndex = reader.GetOrdinal("CreatedAt");

                    int isActiveIndex = reader.GetOrdinal("IsActive");

                    if (reader.Read())
                    {
                        string description = reader.IsDBNull(descriptionIndex)
                            ? null
                            : reader.GetString(descriptionIndex);

                        string createdBy = reader.IsDBNull(createdByIndex)
                            ? null
                            : reader.GetString(createdByIndex);

                        return new PermissionDto(
                                reader.GetInt32(permissionIDIndex),
                                reader.GetString(permissionNameIndex),
                                description,
                                createdBy,
                                reader.GetDateTime(createdAtIndex),
                                reader.GetBoolean(isActiveIndex)
                            );
                    }
                    else
                        return null;
                }
            }
        }


        public static bool DeactivatePermission(int permissionID, string currentUser)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_Permission_Deactivate", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@PermissionID", SqlDbType.Int).Value = permissionID;
                command.Parameters.Add("@PerformedBy", SqlDbType.NVarChar, 100).Value = currentUser;

                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }


        public static bool ReactivatePermission(int permissionID, string currentUser)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_Permission_Reactivate", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@PermissionID", SqlDbType.Int).Value = permissionID;
                command.Parameters.Add("@PerformedBy", SqlDbType.NVarChar, 100).Value = currentUser;

                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}
