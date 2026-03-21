using BusinessHub.Modules.DebtFlow.DTOs.Invoices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.DebtFlow.Repositories.Invoices
{
    public class SupplierInvoicesRepository
    {
        private static readonly string _cs =
            ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static SupplierPaymentInvoiceDto GetInvoiceByReceiptNo(string receiptNo)
        {

            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_SupplierInvoice_GetByReceiptNo", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ReceiptNo", SqlDbType.NVarChar, 50).Value = receiptNo;

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    int InvoiceIDIndex = reader.GetOrdinal("InvoiceID");
                    int SupplierIDIndex = reader.GetOrdinal("SupplierID");

                    int receiptNoIndex = reader.GetOrdinal("ReceiptNo");

                    int InvoiceDateIndex = reader.GetOrdinal("InvoiceDate");

                    int TotalAmountIndex = reader.GetOrdinal("TotalAmount");
                    int InvoiceNoteIndex = reader.GetOrdinal("InvoiceNote");


                    int CreatedByIndex = reader.GetOrdinal("CreatedBy");
                    int CreatedAtIndex = reader.GetOrdinal("CreatedAt");

                    int ReceiverNameIndex = reader.GetOrdinal("ReceiverName");
                    int SignaturePathIndex = reader.GetOrdinal("SignaturePath");

                    if (reader.Read())
                    {
                        string invoiceNote = reader.IsDBNull(InvoiceNoteIndex)
                            ? null
                            : reader.GetString(InvoiceNoteIndex);

                        string signaturePath = reader.IsDBNull(SignaturePathIndex)
                            ? null
                            : reader.GetString(SignaturePathIndex);



                        return new SupplierPaymentInvoiceDto(
                            reader.GetInt32(InvoiceIDIndex),
                            reader.GetInt32(SupplierIDIndex),

                            reader.GetString(receiptNoIndex),
                            reader.GetDateTime(InvoiceDateIndex),

                            reader.GetDecimal(TotalAmountIndex),

                            invoiceNote,
                            reader.GetString(CreatedByIndex),
                            reader.GetDateTime(CreatedAtIndex),
                            reader.GetString(ReceiverNameIndex),
                            signaturePath
                            );
                    }


                    else
                        return null;

                }
            }
        }
    }
}
