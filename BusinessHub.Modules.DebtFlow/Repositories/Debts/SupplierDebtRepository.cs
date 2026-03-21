using BusinessHub.Modules.DebtFlow.DTOs.Debts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace BusinessHub.Modules.DebtFlow.Repositories.Debts
{
    public class SupplierDebtRepository
    {
        private static readonly string _cs =
            ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static SupplierDebtDto GetDebtById(int debtId)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_SupplierDebt_GetByID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@DebtID", debtId);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    int DebtIDIndex = reader.GetOrdinal("DebtID");
                    int SupplierIDIndex = reader.GetOrdinal("SupplierID");
                    int AmountIndex = reader.GetOrdinal("Amount");
                    int DateIndex = reader.GetOrdinal("Date");
                    int noteIndex = reader.GetOrdinal("Note");
                    int CreatedByIndex = reader.GetOrdinal("CreatedBy");
                    int CreatedAtIndex = reader.GetOrdinal("CreatedAt");
                    int UpdatedByIndex = reader.GetOrdinal("UpdatedBy");
                    int UpdatedAtIndex = reader.GetOrdinal("UpdatedAt");


                    if (reader.Read())
                    {
                        string note = reader.IsDBNull(noteIndex)
                            ? null
                            : reader.GetString(noteIndex);

                        string updatedBy = reader.IsDBNull(UpdatedByIndex)
                            ? null
                            : reader.GetString(UpdatedByIndex);

                        DateTime? updatedAt = reader.IsDBNull(UpdatedAtIndex)
                            ? (DateTime?)null
                            : reader.GetDateTime(UpdatedAtIndex);

                        return new SupplierDebtDto(
                                reader.GetInt32(DebtIDIndex),
                                reader.GetInt32(SupplierIDIndex),
                                reader.GetDecimal(AmountIndex),
                                reader.GetDateTime(DateIndex),
                                note,
                                reader.GetString(CreatedByIndex),
                                reader.GetDateTime(CreatedAtIndex),
                                updatedBy,
                                updatedAt

                            );
                    }
                    else
                        return null;
                }
            }
        }


        public static List<SupplierDebtDto> GetDebtsBySupplierID(int supplierId)
        {
            var SuppliersList = new List<SupplierDebtDto>();

            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_SupplierDebt_GetBySupplierID", connection))
            {

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SupplierID", supplierId);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        int DebtIDIndex = reader.GetOrdinal("DebtID");
                        int SupplierIDIndex = reader.GetOrdinal("SupplierID");
                        int AmountIndex = reader.GetOrdinal("Amount");
                        int DateIndex = reader.GetOrdinal("Date");
                        int noteIndex = reader.GetOrdinal("Note");
                        int CreatedByIndex = reader.GetOrdinal("CreatedBy");
                        int CreatedAtIndex = reader.GetOrdinal("CreatedAt");
                        int UpdatedByIndex = reader.GetOrdinal("UpdatedBy");
                        int UpdatedAtIndex = reader.GetOrdinal("UpdatedAt");

                        while (reader.Read())
                        {
                            string note = reader.IsDBNull(noteIndex)
                                ? null
                                : reader.GetString(noteIndex);

                            string updatedBy = reader.IsDBNull(UpdatedByIndex)
                                ? null
                                : reader.GetString(UpdatedByIndex);

                            DateTime? updatedAt = reader.IsDBNull(UpdatedAtIndex)
                                ? (DateTime?)null
                                : reader.GetDateTime(UpdatedAtIndex);

                            SuppliersList.Add(new SupplierDebtDto(
                                    reader.GetInt32(DebtIDIndex),
                                    reader.GetInt32(SupplierIDIndex),
                                    reader.GetDecimal(AmountIndex),
                                    reader.GetDateTime(DateIndex),
                                    note,
                                    reader.GetString(CreatedByIndex),
                                    reader.GetDateTime(CreatedAtIndex),
                                    updatedBy,
                                    updatedAt
                                ));
                        }
                    }

                    return SuppliersList;
                }
            }
        }


        public static int AddDebt(SupplierDebtDto debt)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_SupplierDebt_Add", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@SupplierID", debt.SupplierID);
                command.Parameters.AddWithValue("@Amount", debt.Amount);
                command.Parameters.AddWithValue("@Date", debt.Date);

                command.Parameters.AddWithValue("@Note",
                    (object)debt.Note ?? DBNull.Value);

                command.Parameters.AddWithValue("@CreatedBy", debt.CreatedBy);

                connection.Open();
                var result = command.ExecuteScalar();

                return Convert.ToInt32(result);
            }
        }


        public static bool UpdateDebt(SupplierDebtDto debt)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_SupplierDebt_Update", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@DebtID", debt.DebtID);
                command.Parameters.AddWithValue("@Amount", debt.Amount);
                command.Parameters.AddWithValue("@Date", debt.Date);

                command.Parameters.AddWithValue("@Note",
                    (object)debt.Note ?? DBNull.Value);

                command.Parameters.AddWithValue("@UpdatedBy", debt.UpdatedBy);


                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}
