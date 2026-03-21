using BusinessHub.Modules.Identity.DTOs.RolePermission;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.Identity.Repositories.RolePermission
{
    public class RolePermissionRepository
    {
        private static readonly string _cs =
            ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static int AddRolePermission(RolePermissionDto RolePermission, string currentUser)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_RolePermission_Add", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@RoleID", SqlDbType.Int).Value = RolePermission.RoleID;

                command.Parameters.Add("@PermissionID", SqlDbType.Int).Value = RolePermission.PermissionID;

                command.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 100).Value =
                    (object)currentUser ?? DBNull.Value;

                connection.Open();
                var result = command.ExecuteScalar();

                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        public static List<PermissionSummaryDto> GetPermissionsByRoleID(int roleID)
        {

            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_RolePermission_GetByRoleID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleID;

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    int permissionIDIndex = reader.GetOrdinal("PermissionID");
                    int permissionNameIndex = reader.GetOrdinal("PermissionName");
                    int descriptionIndex = reader.GetOrdinal("Description");

                    var permissions = new List<PermissionSummaryDto>();

                    while (reader.Read())
                    {
                        string description = reader.IsDBNull(descriptionIndex)
                            ? null
                            : reader.GetString(descriptionIndex);

                        permissions.Add(new PermissionSummaryDto(
                            reader.GetInt32(permissionIDIndex),
                            reader.GetString(permissionNameIndex),
                            description
                        ));
                    }

                    return permissions;
                }
            }
        }


        public static List<RoleSummaryDto> GetRolesByPermissionID(int permissionID)
        {

            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_RolePermission_GetByPermissionID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@PermissionID", SqlDbType.Int).Value = permissionID;

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    int roleIDIndex = reader.GetOrdinal("RoleID");
                    int roleNameIndex = reader.GetOrdinal("RoleName");
                    int descriptionIndex = reader.GetOrdinal("Description");

                    var roles = new List<RoleSummaryDto>();

                    while (reader.Read())
                    {
                        string description = reader.IsDBNull(descriptionIndex)
                            ? null
                            : reader.GetString(descriptionIndex);

                        roles.Add(new RoleSummaryDto(
                            reader.GetInt32(roleIDIndex),
                            reader.GetString(roleNameIndex),
                            description
                        ));
                    }

                    return roles;
                }
            }
        }


        public static bool RemoveRolePermission(int roleID, int permissionID, string currentUser)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_RolePermission_Remove", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleID;
                command.Parameters.Add("@PermissionID", SqlDbType.Int).Value = permissionID;
                command.Parameters.Add("@PerformedBy", SqlDbType.NVarChar, 100).Value = currentUser;

                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}
