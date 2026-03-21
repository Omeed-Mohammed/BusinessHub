using BusinessHub.Modules.DebtFlow.DTOs.Payments;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.DebtFlow.Repositories.Payments
{
    public class SupplierPaymentRepository
    {
        private static readonly string _cs =
           ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static string AddSupplierPayment(CreateSupplierPaymentTransactionRequestDto RequestDTO)
        {
            if (RequestDTO == null)
                throw new ArgumentNullException(nameof(RequestDTO));

            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_SupplierPayment_Add", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@SupplierID", SqlDbType.Int).Value = RequestDTO.SupplierID;

                var amountParam = command.Parameters.Add("@Amount", SqlDbType.Decimal);
                amountParam.Precision = 18;
                amountParam.Scale = 2;
                amountParam.Value = RequestDTO.InvoiceAmount;

                command.Parameters.Add("@InvoiceNote", SqlDbType.NVarChar, 250)
                 .Value = (object)RequestDTO.InvoiceNote ?? DBNull.Value;

                command.Parameters.Add("@PaymentNote", SqlDbType.NVarChar, 250)
                 .Value = (object)RequestDTO.PaymentNote ?? DBNull.Value;

                command.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 100)
                .Value = RequestDTO.CreatedBy;

                command.Parameters.Add("@ReceiverName", SqlDbType.NVarChar, 150)
                .Value = RequestDTO.ReceiverName;

                command.Parameters.Add("@SignaturePath", SqlDbType.NVarChar, 300)
                .Value = (object)RequestDTO.SignaturePath ?? DBNull.Value;

                connection.Open();
                var receiptNo = command.ExecuteScalar();

                if (receiptNo == null)
                    throw new Exception("Failed to generate receipt number.");

                return receiptNo.ToString();
            }
        }


        public static List<SupplierPaymentViewDto> GetPaymentsBySupplierID(int supplierID)
        {
            var SupplierPaymentsList = new List<SupplierPaymentViewDto>();


            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_SupplierPayment_GetBySupplierID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@SupplierID", SqlDbType.Int).Value = supplierID;

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    int PaymentIDIndex = reader.GetOrdinal("PaymentID");
                    int InvoiceIDIndex = reader.GetOrdinal("InvoiceID");

                    int receiptNoIndex = reader.GetOrdinal("ReceiptNo");

                    int InvoiceDateIndex = reader.GetOrdinal("InvoiceDate");

                    int TotalAmountIndex = reader.GetOrdinal("TotalAmount");
                    int InvoiceNoteIndex = reader.GetOrdinal("InvoiceNote");
                    int ReceiverNameIndex = reader.GetOrdinal("ReceiverName");
                    int SignaturePathIndex = reader.GetOrdinal("SignaturePath");

                    int CreatedByIndex = reader.GetOrdinal("CreatedBy");
                    int CreatedAtIndex = reader.GetOrdinal("CreatedAt");

                    int PaymentNoteIndex = reader.GetOrdinal("PaymentNote");

                    int UpdatedByIndex = reader.GetOrdinal("UpdatedBy");
                    int UpdatedAtIndex = reader.GetOrdinal("UpdatedAt");

                    while (reader.Read())
                    {
                        string invoiceNote = reader.IsDBNull(InvoiceNoteIndex)
                            ? null
                            : reader.GetString(InvoiceNoteIndex);

                        string paymentNote = reader.IsDBNull(PaymentNoteIndex)
                            ? null
                            : reader.GetString(PaymentNoteIndex);

                        string updatedBy = reader.IsDBNull(UpdatedByIndex)
                            ? null
                            : reader.GetString(UpdatedByIndex);

                        DateTime? updatedAt = reader.IsDBNull(UpdatedAtIndex)
                            ? (DateTime?)null
                            : reader.GetDateTime(UpdatedAtIndex);

                        string signaturePath = reader.IsDBNull(SignaturePathIndex)
                            ? null
                            : reader.GetString(SignaturePathIndex);



                        SupplierPaymentsList.Add(new SupplierPaymentViewDto(
                            reader.GetInt32(PaymentIDIndex),
                            reader.GetInt32(InvoiceIDIndex),

                            reader.GetString(receiptNoIndex),
                            reader.GetDateTime(InvoiceDateIndex),

                            reader.GetDecimal(TotalAmountIndex),

                            invoiceNote,
                            reader.GetString(ReceiverNameIndex),
                            signaturePath,
                            reader.GetString(CreatedByIndex),

                            reader.GetDateTime(CreatedAtIndex),

                            paymentNote,
                            updatedBy,
                            updatedAt
                            ));

                    }

                }

                return SupplierPaymentsList;
            }
        }


        public static bool UpdateSupplierPayments(UpdateSupplierPaymentRequestDto paymentDTO)
        {

            if (paymentDTO == null)
                throw new ArgumentNullException(nameof(paymentDTO));

            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_SupplierPayment_Update", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@PaymentID", SqlDbType.Int)
                .Value = paymentDTO.PaymentID;

                command.Parameters.Add("@PaymentNote", SqlDbType.NVarChar, 250).Value =
                    (object)paymentDTO.PaymentNote ?? DBNull.Value;

                command.Parameters.Add("@UpdatedBy", SqlDbType.NVarChar, 100).Value =
                   paymentDTO.UpdatedBy;

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}
