using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.DebtFlow.DTOs.Invoices
{
    public class SupplierPaymentInvoiceDto
    {
        public int InvoiceID { get; set; }
        public int SupplierID { get; set; }
        public string ReceiptNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal InvoiceAmount { get; set; }
        public string InvoiceNote { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ReceiverName { get; set; }
        public string SignaturePath { get; set; }


        public SupplierPaymentInvoiceDto(int invoiceID, int supplierID, string receiptNo,
            DateTime invoiceDate, decimal invoiceAmount, string invoiceNote, string createdBy,
            DateTime createdAt, string receiverName, string signaturePath)
        {
            InvoiceID = invoiceID;
            SupplierID = supplierID;
            ReceiptNo = receiptNo;
            InvoiceDate = invoiceDate;
            InvoiceAmount = invoiceAmount;
            InvoiceNote = invoiceNote;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
            ReceiverName = receiverName;
            SignaturePath = signaturePath;

        }
    }
}
