using BusinessHub.Modules.HR.DTOs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.HR.Repositories
{
    public class EmployeeRepository
    {
        private static readonly string _cs =
          ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static int AddEmployee(EmployeeDto newRequest)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("hr.SP_Employee_Add", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@PersonID", SqlDbType.Int).Value = newRequest.PersonID;
                command.Parameters.Add("@JobTitle", SqlDbType.NVarChar, 100).Value = newRequest.JobTitle;
                command.Parameters.Add("@HireDate", SqlDbType.Date).Value = newRequest.HireDate;
                command.Parameters.Add("@EmployeeLogin", SqlDbType.NVarChar, 50).Value = newRequest.EmployeeLogin;
                command.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 100).Value = newRequest.CreatedBy;

                connection.Open();

                return (int)command.ExecuteScalar();
            }
        }

        public static bool UpdateEmployee(EmployeeDto EmployeeUpdate)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("hr.SP_Employee_Update", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = EmployeeUpdate.EmployeeID;
                command.Parameters.Add("@JobTitle", SqlDbType.NVarChar, 100).Value = EmployeeUpdate.JobTitle;
                command.Parameters.Add("@HireDate", SqlDbType.Date).Value = EmployeeUpdate.HireDate;
                command.Parameters.Add("@EmployeeLogin", SqlDbType.NVarChar, 50).Value = EmployeeUpdate.EmployeeLogin;
                command.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 100).Value = EmployeeUpdate.UpdatedBy; 


                connection.Open();
                int rows = command.ExecuteNonQuery();
                return rows > 0;
            }
        }


        public static EmployeeDto GetEmployeeById(int EmployeeId , bool IsActive , string CurrentUser)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("hr.SP_Employee_GetByID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = EmployeeId;
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = IsActive;
                command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = CurrentUser;

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    int employeeIDIndex = reader.GetOrdinal("EmployeeID");
                    int personIDIndex = reader.GetOrdinal("PersonID");
                    int jobTitleIndex = reader.GetOrdinal("JobTitle");
                    int hireDateIndex = reader.GetOrdinal("HireDate");
                    int employeeLoginIndex = reader.GetOrdinal("EmployeeLogin");
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

                        return new EmployeeDto(
                            reader.GetInt32(employeeIDIndex),
                            reader.GetInt32(personIDIndex),
                            reader.GetString(jobTitleIndex),
                            reader.GetDateTime(hireDateIndex),
                            reader.GetBoolean(isActiveIndex),
                            reader.GetDateTime(createdAtIndex),
                            reader.GetString(createdByIndex),
                            updatedAt,
                            updatedBy,
                            reader.GetString(employeeLoginIndex)
                        );
                    }

                    else
                        return null;
                }
            }
        }

        public static List<EmployeeDto> GetAllEmployees(bool? isActive)
        {
            var employees = new List<EmployeeDto>();

            using (SqlConnection conn = new SqlConnection(_cs))
            {
                using (SqlCommand cmd = new SqlCommand("hr.SP_Employee_GetAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value =
                    isActive.HasValue ? (object)isActive.Value : DBNull.Value;

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int employeeIDIndex = reader.GetOrdinal("EmployeeID");
                        int personIDIndex = reader.GetOrdinal("PersonID");
                        int jobTitleIndex = reader.GetOrdinal("JobTitle");
                        int hireDateIndex = reader.GetOrdinal("HireDate");
                        int employeeLoginIndex = reader.GetOrdinal("EmployeeLogin");
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

                            employees.Add(new EmployeeDto(
                                reader.GetInt32(employeeIDIndex),
                                reader.GetInt32(personIDIndex),
                                reader.GetString(jobTitleIndex),
                                reader.GetDateTime(hireDateIndex),
                                reader.GetBoolean(isActiveIndex),
                                reader.GetDateTime(createdAtIndex),
                                reader.GetString(createdByIndex),
                                updatedAt,
                                updatedBy,
                                reader.GetString(employeeLoginIndex)
                            ));
                        }
                    }
                }
            }

            return employees;
        }

        public static EmployeeDto GetEmployeeByLogin(string login, bool? isActive, string currentUser)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("hr.SP_Employee_GetByLogin", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@EmployeeLogin", SqlDbType.NVarChar, 50).Value = login;
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value =
                    isActive.HasValue ? (object)isActive.Value : DBNull.Value;
                command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    int employeeIDIndex = reader.GetOrdinal("EmployeeID");
                    int personIDIndex = reader.GetOrdinal("PersonID");
                    int jobTitleIndex = reader.GetOrdinal("JobTitle");
                    int hireDateIndex = reader.GetOrdinal("HireDate");
                    int employeeLoginIndex = reader.GetOrdinal("EmployeeLogin");
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

                        return new EmployeeDto(
                            reader.GetInt32(employeeIDIndex),
                            reader.GetInt32(personIDIndex),
                            reader.GetString(jobTitleIndex),
                            reader.GetDateTime(hireDateIndex),
                            reader.GetBoolean(isActiveIndex),
                            reader.GetDateTime(createdAtIndex),
                            reader.GetString(createdByIndex),
                            updatedAt,
                            updatedBy,
                            reader.GetString(employeeLoginIndex)
                        );
                    }

                    return null;
                }
            }
        }

        public static List<EmployeeSearchResultDto> SearchEmployees(string firstName, string lastName, bool? isActive, string currentUser)
        {
            var list = new List<EmployeeSearchResultDto>();

            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("hr.SP_Employee_SearchByName", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 100).Value =
                    string.IsNullOrWhiteSpace(firstName) ? (object)DBNull.Value : firstName;

                command.Parameters.Add("@LastName", SqlDbType.NVarChar, 100).Value =
                    string.IsNullOrWhiteSpace(lastName) ? (object)DBNull.Value : lastName;

                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value =
                    isActive.HasValue ? (object)isActive.Value : DBNull.Value;

                command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    int employeeIDIndex = reader.GetOrdinal("EmployeeID");
                    int personIDIndex = reader.GetOrdinal("PersonID");
                    int firstNameIndex = reader.GetOrdinal("FirstName");
                    int lastNameIndex = reader.GetOrdinal("LastName");
                    int jobTitleIndex = reader.GetOrdinal("JobTitle");
                    int hireDateIndex = reader.GetOrdinal("HireDate");
                    int employeeLoginIndex = reader.GetOrdinal("EmployeeLogin");
                    int isActiveIndex = reader.GetOrdinal("IsActive");

                    while (reader.Read())
                    {
                        list.Add(new EmployeeSearchResultDto(
                            reader.GetInt32(employeeIDIndex),
                            reader.GetInt32(personIDIndex),
                            reader.GetString(firstNameIndex),
                            reader.GetString(lastNameIndex),
                            reader.GetString(jobTitleIndex),
                            reader.GetDateTime(hireDateIndex),
                            reader.GetString(employeeLoginIndex),
                            reader.GetBoolean(isActiveIndex)
                        ));
                    }
                }
            }

            return list;
        }

        public static (EmployeeDto employee, List<EmployeeRoleDto> roles, List<EmployeeDepartmentInfoDto> departments)
          GetEmployeeProfile(int employeeId, bool? isActive, string currentUser)
        {
            EmployeeDto employee = null;
            var roles = new List<EmployeeRoleDto>();
            var departments = new List<EmployeeDepartmentInfoDto>();

            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("hr.SP_Employee_GetProfile", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = employeeId;
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value =
                    isActive.HasValue ? (object)isActive.Value : DBNull.Value;
                command.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    // 1. Employee
                    if (reader.Read())
                    {
                        int employeeIDIndex = reader.GetOrdinal("EmployeeID");
                        int personIDIndex = reader.GetOrdinal("PersonID");
                        int jobTitleIndex = reader.GetOrdinal("JobTitle");
                        int hireDateIndex = reader.GetOrdinal("HireDate");
                        int employeeLoginIndex = reader.GetOrdinal("EmployeeLogin");
                        int isActiveIndex = reader.GetOrdinal("IsActive");
                        int createdAtIndex = reader.GetOrdinal("CreatedAt");
                        int createdByIndex = reader.GetOrdinal("CreatedBy");
                        int updatedAtIndex = reader.GetOrdinal("UpdatedAt");
                        int updatedByIndex = reader.GetOrdinal("UpdatedBy");

                        DateTime? updatedAt = reader.IsDBNull(updatedAtIndex)
                            ? (DateTime?)null
                            : reader.GetDateTime(updatedAtIndex);

                        string updatedBy = reader.IsDBNull(updatedByIndex)
                            ? string.Empty
                            : reader.GetString(updatedByIndex);

                        employee = new EmployeeDto(
                            reader.GetInt32(employeeIDIndex),
                            reader.GetInt32(personIDIndex),
                            reader.GetString(jobTitleIndex),
                            reader.GetDateTime(hireDateIndex),
                            reader.GetBoolean(isActiveIndex),
                            reader.GetDateTime(createdAtIndex),
                            reader.GetString(createdByIndex),
                            updatedAt,
                            updatedBy,
                            reader.GetString(employeeLoginIndex)
                        );
                    }

                    // 2. Roles
                    if (reader.NextResult())
                    {
                        int idIndex = reader.GetOrdinal("EmployeeRoleID");
                        int roleIDIndex = reader.GetOrdinal("RoleID");
                        int roleNameIndex = reader.GetOrdinal("RoleName");
                        int isActiveIndex = reader.GetOrdinal("IsActive");
                        int startIndex = reader.GetOrdinal("StartDate");
                        int endIndex = reader.GetOrdinal("EndDate");
                        int assignedAtIndex = reader.GetOrdinal("AssignedAt");
                        int assignedByIndex = reader.GetOrdinal("AssignedBy");
                        int revokedAtIndex = reader.GetOrdinal("RevokedAt");
                        int revokedByIndex = reader.GetOrdinal("RevokedBy");

                        while (reader.Read())
                        {
                            roles.Add(new EmployeeRoleDto(
                                reader.GetInt32(idIndex),
                                reader.GetInt32(roleIDIndex),
                                reader.GetString(roleNameIndex),
                                reader.GetBoolean(isActiveIndex),
                                reader.GetDateTime(startIndex),
                                reader.IsDBNull(endIndex) ? (DateTime?)null : reader.GetDateTime(endIndex),
                                reader.GetDateTime(assignedAtIndex),
                                reader.GetString(assignedByIndex),
                                reader.IsDBNull(revokedAtIndex) ? (DateTime?)null : reader.GetDateTime(revokedAtIndex),
                                reader.IsDBNull(revokedByIndex) ? string.Empty : reader.GetString(revokedByIndex)
                            ));
                        }
                    }

                    // 3. Departments
                    if (reader.NextResult())
                    {
                        int idIndex = reader.GetOrdinal("EmployeeDepartmentID");
                        int empIdIndex = reader.GetOrdinal("EmployeeID");
                        int deptIDIndex = reader.GetOrdinal("DepartmentID");
                        int deptNameIndex = reader.GetOrdinal("DepartmentName");
                        int startIndex = reader.GetOrdinal("StartDate");
                        int endIndex = reader.GetOrdinal("EndDate");
                        int createdAtIndex = reader.GetOrdinal("CreatedAt");
                        int createdByIndex = reader.GetOrdinal("CreatedBy");

                        while (reader.Read())
                        {
                            departments.Add(new EmployeeDepartmentInfoDto(
                                reader.GetInt32(idIndex),
                                reader.GetInt32(empIdIndex),
                                reader.GetInt32(deptIDIndex),
                                reader.GetString(deptNameIndex),
                                reader.GetDateTime(startIndex),
                                reader.IsDBNull(endIndex) ? (DateTime?)null : reader.GetDateTime(endIndex),
                                reader.GetDateTime(createdAtIndex),
                                reader.GetString(createdByIndex)
                            ));
                        }
                    }
                }
            }

            return (employee, roles, departments);
        }


        public static bool DeactivateEmployee(int employeeId, string currentUser)
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand("hr.SP_Employee_Deactivate", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = employeeId;
                cmd.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }


        public static bool ReactivateEmployee(int employeeId, string currentUser)
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand("hr.SP_Employee_Reactivate", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = employeeId;
                cmd.Parameters.Add("@CurrentUser", SqlDbType.NVarChar, 100).Value = currentUser;

                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }
    }
}
