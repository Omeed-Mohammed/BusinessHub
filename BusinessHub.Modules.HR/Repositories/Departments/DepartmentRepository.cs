using BusinessHub.Core.Common;
using BusinessHub.Modules.HR.DTOs.Departments;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.HR.Repositories.Departments
{
    public class DepartmentRepository
    {
        private static readonly string _cs =
           ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static int AddDepartment(DepartmentDto departmentRequest)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("hr.SP_Department_Add", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@DepartmentName", SqlDbType.NVarChar, 100).Value = departmentRequest.DepartmentName;
                command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = departmentRequest.CreatedBy;

                connection.Open();

                return (int)command.ExecuteScalar();
            }
        }

        public static bool UpdateDepartment(DepartmentDto departmentUpdate)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("hr.SP_Department_Update", connection))
            {
                command.CommandType = CommandType.StoredProcedure;


                command.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = departmentUpdate.DepartmentID;

                command.Parameters.Add("@DepartmentName", SqlDbType.NVarChar, 100).Value = departmentUpdate.DepartmentName;

                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = departmentUpdate.IsActive;

                command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = departmentUpdate.UpdatedBy;


                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
        }

        public static List<DepartmentDto> GetAllDepartments()
        {
            var departments = new List<DepartmentDto>();

            using (SqlConnection conn = new SqlConnection(_cs))
            {
                using (SqlCommand cmd = new SqlCommand("hr.SP_Department_GetAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int departmentIDIndex = reader.GetOrdinal("DepartmentID");

                        int departmentNameIndex = reader.GetOrdinal("DepartmentName");
                        int isActiveIndex = reader.GetOrdinal("IsActive");


                        int createdAtIndex = reader.GetOrdinal("CreatedAt");
                        int createdByIndex = reader.GetOrdinal("CreatedBy");

                        int updatedAtIndex = reader.GetOrdinal("UpdatedAt");
                        int updatedByIndex = reader.GetOrdinal("UpdatedBy");


                        while (reader.Read())
                        {
                            DateTime? updatedAt = reader.IsDBNull(updatedAtIndex)
                                ? (DateTime?)null
                                : reader.GetDateTime(updatedAtIndex);

                            string updatedBy = reader.IsDBNull(updatedByIndex)
                                ? string.Empty
                                : reader.GetString(updatedByIndex);


                            departments.Add(new DepartmentDto(
                                reader.GetInt32(departmentIDIndex),
                                reader.GetString(departmentNameIndex),
                                reader.GetBoolean(isActiveIndex),
                                reader.GetDateTime(createdAtIndex),
                                reader.GetString(createdByIndex),
                                updatedAt,
                                updatedBy
                            ));
                        }
                    }
                }
            }

            return departments;
        }

        public static DepartmentDto GetDepartmentByID(int DepartmentID)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("hr.SP_Department_GetByID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = DepartmentID;

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    int departmentIDIndex = reader.GetOrdinal("DepartmentID");

                    int departmentNameIndex = reader.GetOrdinal("DepartmentName");
                    int isActiveIndex = reader.GetOrdinal("IsActive");


                    int createdAtIndex = reader.GetOrdinal("CreatedAt");
                    int createdByIndex = reader.GetOrdinal("CreatedBy");

                    int updatedAtIndex = reader.GetOrdinal("UpdatedAt");
                    int updatedByIndex = reader.GetOrdinal("UpdatedBy");

                    if (reader.Read())
                    {
                        DateTime? updatedAt = reader.IsDBNull(updatedAtIndex)
                                ? (DateTime?)null
                                : reader.GetDateTime(updatedAtIndex);

                        string updatedBy = reader.IsDBNull(updatedByIndex)
                            ? string.Empty
                            : reader.GetString(updatedByIndex);

                        return new DepartmentDto(
                                reader.GetInt32(departmentIDIndex),
                                reader.GetString(departmentNameIndex),
                                reader.GetBoolean(isActiveIndex),
                                reader.GetDateTime(createdAtIndex),
                                reader.GetString(createdByIndex),
                                updatedAt,
                                updatedBy
                            );
                    }
                    else
                        return null;
                }
            }
        }

        public static DepartmentDto GetDepartmentByName(string departmentName)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("hr.SP_Department_GetByName", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@DepartmentName", SqlDbType.NVarChar,100).Value = departmentName;

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    int departmentIDIndex = reader.GetOrdinal("DepartmentID");

                    int departmentNameIndex = reader.GetOrdinal("DepartmentName");
                    int isActiveIndex = reader.GetOrdinal("IsActive");


                    int createdAtIndex = reader.GetOrdinal("CreatedAt");
                    int createdByIndex = reader.GetOrdinal("CreatedBy");

                    int updatedAtIndex = reader.GetOrdinal("UpdatedAt");
                    int updatedByIndex = reader.GetOrdinal("UpdatedBy");

                    if (reader.Read())
                    {
                        DateTime? updatedAt = reader.IsDBNull(updatedAtIndex)
                                ? (DateTime?)null
                                : reader.GetDateTime(updatedAtIndex);

                        string updatedBy = reader.IsDBNull(updatedByIndex)
                            ? string.Empty
                            : reader.GetString(updatedByIndex);

                        return new DepartmentDto(
                                reader.GetInt32(departmentIDIndex),
                                reader.GetString(departmentNameIndex),
                                reader.GetBoolean(isActiveIndex),
                                reader.GetDateTime(createdAtIndex),
                                reader.GetString(createdByIndex),
                                updatedAt,
                                updatedBy
                            );
                    }
                    else
                        return null;
                }
            }
        }
    }
}
