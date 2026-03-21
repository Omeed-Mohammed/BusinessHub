using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.DebtFlow.DTOs.Payments
{
    public class CreateSupplierPaymentTransactionRequestDto
    {
        public int SupplierID { get; set; }
        public decimal InvoiceAmount { get; set; }
        public string InvoiceNote { get; set; }
        public string PaymentNote { get; set; }
        public string CreatedBy { get; set; }
        public string ReceiverName { get; set; }
        public string SignaturePath { get; set; }


        public CreateSupplierPaymentTransactionRequestDto(int supplierID, decimal invoiceAmount, string invoiceNote, string paymentNote, string createdBy, string receiverName, string signaturePath)
        {
            SupplierID = supplierID;
            InvoiceAmount = invoiceAmount;
            InvoiceNote = invoiceNote;
            PaymentNote = paymentNote;
            CreatedBy = createdBy;
            ReceiverName = receiverName;
            SignaturePath = signaturePath;



        }
    }
}
