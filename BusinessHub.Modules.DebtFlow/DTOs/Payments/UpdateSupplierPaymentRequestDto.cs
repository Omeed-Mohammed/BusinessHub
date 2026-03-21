using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.DebtFlow.DTOs.Payments
{
    public class UpdateSupplierPaymentRequestDto
    {
        public int PaymentID { get; set; }
        public string PaymentNote { get; set; }
        public string UpdatedBy { get; set; }

        public UpdateSupplierPaymentRequestDto(int paymentID, string paymentNote, string updatedBy)
        {
            PaymentID = paymentID;
            PaymentNote = paymentNote;
            UpdatedBy = updatedBy;
        }
    }
}
