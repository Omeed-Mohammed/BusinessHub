using BusinessHub.Modules.DebtFlow.DTOs.Supplier;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.DebtFlow.Repositories.Supplier
{
    public class SupplierRepository
    {
        private static readonly string _cs =
            ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static List<SupplierDto> GetAllSuppliers(bool? isActive = true)
        {
            var SuppliersList = new List<SupplierDto>();

            using (SqlConnection conn = new SqlConnection(_cs))
            {
                using (SqlCommand cmd = new SqlCommand("SP_Supplier_GetAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IsActive", isActive.HasValue ?
                    (object)isActive.Value : DBNull.Value);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int addressIndex = reader.GetOrdinal("Address");
                        int noteIndex = reader.GetOrdinal("Note");

                        int supplierIdIndex = reader.GetOrdinal("SupplierID");
                        int fullNameIndex = reader.GetOrdinal("FullName");
                        int phoneIndex = reader.GetOrdinal("Phone");
                        int isActiveIndex = reader.GetOrdinal("IsActive");


                        while (reader.Read())
                        {
                            string address = reader.IsDBNull(addressIndex)
                                ? null
                                : reader.GetString(addressIndex);

                            string note = reader.IsDBNull(noteIndex)
                                ? null
                                : reader.GetString(noteIndex);

                            SuppliersList.Add(new SupplierDto(
                                reader.GetInt32(supplierIdIndex),
                                reader.GetString(fullNameIndex),
                                reader.GetString(phoneIndex),
                                address,
                                note,
                                reader.GetBoolean(isActiveIndex)
                            ));
                        }
                    }
                }
            }

            return SuppliersList;
        }

        public static SupplierDto GetSupplierById(int supplierId)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_Supplier_GetByID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SupplierID", supplierId);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    int supplierIdIndex = reader.GetOrdinal("SupplierID");
                    int fullNameIndex = reader.GetOrdinal("FullName");
                    int phoneIndex = reader.GetOrdinal("Phone");
                    int isActiveIndex = reader.GetOrdinal("IsActive");
                    int addressIndex = reader.GetOrdinal("Address");
                    int noteIndex = reader.GetOrdinal("Note");

                    if (reader.Read())
                    {
                        string address = reader.IsDBNull(addressIndex)
                               ? null
                               : reader.GetString(addressIndex);

                        string note = reader.IsDBNull(noteIndex)
                            ? null
                            : reader.GetString(noteIndex);

                        return new SupplierDto(
                                reader.GetInt32(supplierIdIndex),
                                reader.GetString(fullNameIndex),
                                reader.GetString(phoneIndex),
                                address,
                                note,
                                reader.GetBoolean(isActiveIndex)
                            );
                    }
                    else
                        return null;
                }
            }
        }

        public static int AddSupplier(SupplierDto supplier)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_Supplier_Add", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@FullName", supplier.FullName);
                command.Parameters.AddWithValue("@Phone", supplier.Phone);

                command.Parameters.AddWithValue("@Address",
                    (object)supplier.Address ?? DBNull.Value);

                command.Parameters.AddWithValue("@Note",
                    (object)supplier.Note ?? DBNull.Value);

                connection.Open();
                var result = command.ExecuteScalar();

                return Convert.ToInt32(result);
            }
        }

        public static bool UpdateSupplier(SupplierDto supplier)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_Supplier_Update", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@SupplierID", supplier.SupplierID);
                command.Parameters.AddWithValue("@FullName", supplier.FullName);
                command.Parameters.AddWithValue("@Phone", supplier.Phone);

                command.Parameters.AddWithValue("@Address",
                    (object)supplier.Address ?? DBNull.Value);

                command.Parameters.AddWithValue("@Note",
                    (object)supplier.Note ?? DBNull.Value);


                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }


        public static bool DeactivateSupplier(int supplierId)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_Supplier_Deactivate", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SupplierID", supplierId);

                connection.Open();

                int rowsAffected = (int)command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }


        public static bool ReactivateSupplier(int supplierId)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_Supplier_Reactivate", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SupplierID", supplierId);

                connection.Open();

                int rowsAffected = (int)command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}
