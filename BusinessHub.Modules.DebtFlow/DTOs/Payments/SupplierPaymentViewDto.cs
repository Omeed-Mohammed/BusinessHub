using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.DebtFlow.DTOs.Payments
{
    public class SupplierPaymentViewDto
    {
        public int PaymentID { get; set; }
        public int InvoiceID { get; set; }
        public string ReceiptNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal InvoiceAmount { get; set; }
        public string InvoiceNote { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ReceiverName { get; set; }
        public string SignaturePath { get; set; }

        public string PaymentNote { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }


        public SupplierPaymentViewDto(int paymentID, int invoiceID, string receiptNo,
            DateTime invoiceDate, decimal invoiceAmount, string invoiceNote,
            string receiverName, string signaturePath, string createdBy, DateTime createdAt, string paymentNote,
            string updatedBy, DateTime? updatedAt)
        {
            PaymentID = paymentID;
            InvoiceID = invoiceID;
            ReceiptNo = receiptNo;
            InvoiceDate = invoiceDate;
            InvoiceAmount = invoiceAmount;
            InvoiceNote = invoiceNote;
            ReceiverName = receiverName;
            SignaturePath = signaturePath;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
            PaymentNote = paymentNote;
            UpdatedBy = updatedBy;
            UpdatedAt = updatedAt;
        }
    }
}
