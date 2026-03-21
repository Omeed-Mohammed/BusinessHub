using BusinessHub.Modules.DebtFlow.DTOs.Debts;
using BusinessHub.Modules.DebtFlow.Repositories.Debts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.DebtFlow.Services.Debts
{
    public class SupplierDebtService
    {
        public static SupplierDebtDto GetDebtById(int debtId)
        {
            if (debtId <= 0)
                return null;

            return SupplierDebtRepository.GetDebtById(debtId);
        }

        public static List<SupplierDebtDto> GetDebtsBySupplierID(int supplierId)
        {
            if (supplierId <= 0)
                return new List<SupplierDebtDto>();

            return SupplierDebtRepository.GetDebtsBySupplierID(supplierId);
        }


        public static int AddDebt(CreateSupplierDebtRequestDto DebtRequest)
        {
            if (DebtRequest.SupplierID <= 0 ||
                DebtRequest.Amount <= 0 ||
                string.IsNullOrWhiteSpace(DebtRequest.CreatedBy))
                return -1;

            var supplierDebtDTO = new SupplierDebtDto(
             0,
            DebtRequest.SupplierID,
            DebtRequest.Amount,
            DebtRequest.Date,
            DebtRequest.Note,
            DebtRequest.CreatedBy,
            null,
            null,
            null);

            return SupplierDebtRepository.AddDebt(supplierDebtDTO);
        }


        public static bool UpdateDebt(UpdateSupplierDebtRequestDto DebtRequest)
        {
            if (DebtRequest.DebtID <= 0 ||
                DebtRequest.Amount <= 0 ||
                string.IsNullOrWhiteSpace(DebtRequest.UpdatedBy))
                return false;

            var existing = SupplierDebtRepository.GetDebtById(DebtRequest.DebtID);
            if (existing == null)
                return false;

            var supplierDebtDTO = new SupplierDebtDto(
                DebtRequest.DebtID,
                existing.SupplierID,
                DebtRequest.Amount,
                DebtRequest.Date,
                DebtRequest.Note,
                existing.CreatedBy,
                existing.CreatedAt,
                DebtRequest.UpdatedBy,
                null);

            return SupplierDebtRepository.UpdateDebt(supplierDebtDTO);
        }
    }
}
