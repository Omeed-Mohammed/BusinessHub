using BusinessHub.Modules.DebtFlow.DTOs.Payments;
using BusinessHub.Modules.DebtFlow.Repositories.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.DebtFlow.Services.Payments
{
    public class SupplierPaymentService
    {
        public static string AddSupplierPayment(CreateSupplierPaymentTransactionRequestDto RequestDTO)
        {
            if (RequestDTO.SupplierID <= 0 ||
                RequestDTO.InvoiceAmount <= 0 ||
                string.IsNullOrWhiteSpace(RequestDTO.ReceiverName) ||
                string.IsNullOrWhiteSpace(RequestDTO.CreatedBy))
                throw new ArgumentException("Invalid payment request.");


            return SupplierPaymentRepository.AddSupplierPayment(RequestDTO);
        }


        public static List<SupplierPaymentViewDto> GetPaymentsBySupplierID(int supplierID)
        {
            if (supplierID <= 0)
                return new List<SupplierPaymentViewDto>();

            return SupplierPaymentRepository.GetPaymentsBySupplierID(supplierID);
        }


        public static bool UpdateSupplierPayments(UpdateSupplierPaymentRequestDto paymentDTO)
        {
            if (paymentDTO.PaymentID <= 0 ||
                string.IsNullOrWhiteSpace(paymentDTO.UpdatedBy))
                return false;

            return SupplierPaymentRepository.UpdateSupplierPayments(paymentDTO);
        }
    }
}
