using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.DebtFlow.DTOs.Debts
{
    public class UpdateSupplierDebtRequestDto
    {
        public int DebtID { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public string UpdatedBy { get; set; }

        public UpdateSupplierDebtRequestDto(int debtID, decimal amount, DateTime date, string note, string updatedBy)
        {
            DebtID = debtID;
            Amount = amount;
            Date = date;
            Note = note;
            UpdatedBy = updatedBy;
        }
    }
}
