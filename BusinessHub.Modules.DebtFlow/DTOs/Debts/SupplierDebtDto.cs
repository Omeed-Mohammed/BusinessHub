using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.DebtFlow.DTOs.Debts
{
    public class SupplierDebtDto
    {
        public int DebtID { get; set; }
        public int SupplierID { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public SupplierDebtDto(
            int debtID,
            int supplierID,
            decimal amount,
            DateTime date,
            string note,
            string createdBy,
            DateTime? createdAt,
            string updatedBy,
            DateTime? updatedAt
            )
        {
            this.DebtID = debtID;
            this.SupplierID = supplierID;
            this.Amount = amount;
            this.Date = date;
            this.Note = note;
            this.CreatedBy = createdBy;
            this.CreatedAt = createdAt;
            this.UpdatedBy = updatedBy;
            this.UpdatedAt = updatedAt;

        }
    }
}
