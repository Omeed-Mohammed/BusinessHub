using BusinessHub.Modules.DebtFlow.DTOs.Invoices;
using BusinessHub.Modules.DebtFlow.Repositories.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.DebtFlow.Services.Invoices
{
    public class SupplierInvoiceService
    {
        public static SupplierPaymentInvoiceDto GetInvoiceByReceiptNo(string receiptNo)
        {
            if (string.IsNullOrEmpty(receiptNo))
                throw new ArgumentNullException(nameof(receiptNo));

            return SupplierInvoicesRepository.GetInvoiceByReceiptNo(receiptNo);
        }
    }
}
