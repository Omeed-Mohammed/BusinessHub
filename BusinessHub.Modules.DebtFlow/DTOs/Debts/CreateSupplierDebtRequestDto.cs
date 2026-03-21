using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.DebtFlow.DTOs.Debts
{
    public class CreateSupplierDebtRequestDto
    {
        public int SupplierID { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public string CreatedBy { get; set; }

        public CreateSupplierDebtRequestDto(int supplierID, decimal amount, DateTime date, string note, string createdBy)
        {
            SupplierID = supplierID;
            Amount = amount;
            Date = date;
            Note = note;
            CreatedBy = createdBy;
        }
    }
}
