using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.DebtFlow.DTOs.Payments
{
    public class SupplierPaymentDto
    {
        public int PaymentID { get; set; }
        public int SupplierID { get; set; }
        public int InvoiceID { get; set; }
        // public DateTime Date { get; set; }
        public string PaymentNote { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        //public string UpdatedBy { get; set; }
        //public DateTime? UpdatedAt { get; set; }



        public SupplierPaymentDto(
            int paymentID,
            int supplierID,
            int invoiceID,
            string paymentNote,
            string updatedBy,
            DateTime updatedAt

            )
        {
            this.PaymentID = paymentID;
            this.SupplierID = supplierID;
            this.InvoiceID = invoiceID;
            this.PaymentNote = paymentNote;
            this.UpdatedBy = updatedBy;
            this.UpdatedAt = updatedAt;


        }
    }
}
