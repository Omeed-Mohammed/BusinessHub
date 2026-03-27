using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessHub.Modules.HR.DTOs;

namespace BusinessHub.Modules.HR.Repositories
{
    public class EmployeeDepartmentRepository
    {
        private static readonly string _cs =
           ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static int AssignEmployeeDepartment(EmployeeDepartmentDto dto)
        {
            if (dto == null)
                throw new ArgumentException("Invalid data");

            using (SqlConnection conn = new SqlConnection(_cs))
            using (SqlCommand cmd = new SqlCommand("hr.SP_EmployeeDepartment_Assign", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = dto.EmployeeID;
                cmd.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = dto.DepartmentID;
                cmd.Parameters.Add("@StartDate", SqlDbType.Date).Value = dto.StartDate;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 100).Value = dto.CreatedBy;

                conn.Open();

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public static List<EmployeeDepartmentDto> GetByEmployeeID(int employeeID, bool? isActive, string currentUser)
        {
            var list = new List<EmployeeDepartmentDto>();

            using (SqlConnection conn = new SqlConnection(_cs))
            using (SqlCommand cmd = new SqlCommand("hr.SP_EmployeeDepartments_GetByEmployeeID", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = employeeID;

                cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value =
                    isActive.HasValue ? (object)isActive.Value : DBNull.Value;

                cmd.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    int idIndex = reader.GetOrdinal("EmployeeDepartmentID");
                    int empIdIndex = reader.GetOrdinal("EmployeeID");
                    int depIdIndex = reader.GetOrdinal("DepartmentID");
                    int startIndex = reader.GetOrdinal("StartDate");
                    int endIndex = reader.GetOrdinal("EndDate");
                    int createdAtIndex = reader.GetOrdinal("CreatedAt");
                    int createdByIndex = reader.GetOrdinal("CreatedBy");
                    int depNameIndex = reader.GetOrdinal("DepartmentName");

                    while (reader.Read())
                    {
                        list.Add(new EmployeeDepartmentDto(
                            reader.GetInt32(idIndex),
                            reader.GetInt32(empIdIndex),
                            reader.GetInt32(depIdIndex),
                            reader.GetDateTime(startIndex),
                            reader.IsDBNull(endIndex) ? (DateTime?)null : reader.GetDateTime(endIndex),
                            reader.GetDateTime(createdAtIndex),
                            reader.GetString(createdByIndex),
                            reader.GetString(depNameIndex)
                        ));
                    }
                }
            }

            return list;
        }
    }
}
