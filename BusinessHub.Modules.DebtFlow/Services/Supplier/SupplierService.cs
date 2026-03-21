using BusinessHub.Modules.DebtFlow.DTOs.Requests;
using BusinessHub.Modules.DebtFlow.DTOs.Supplier;
using BusinessHub.Modules.DebtFlow.Repositories.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.DebtFlow.Services.Supplier
{
    public class SupplierService
    {
        public static List<SupplierDto> GetAllSuppliers(bool? isActive = true)
        {
            return SupplierRepository.GetAllSuppliers(isActive);
        }

        public static SupplierDto GetSupplierById(int supplierId)
        {
            SupplierDto supplierDTO = SupplierRepository.GetSupplierById(supplierId);

            return supplierDTO;
        }

        public static int AddSupplier(CreateSupplierRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.FullName) ||
                string.IsNullOrWhiteSpace(request.Phone))
                return -1;

            var supplierDTO = new SupplierDto(
                0,
                request.FullName,
                request.Phone,
                request.Address,
                request.Note,
                true);

            return SupplierRepository.AddSupplier(supplierDTO);
        }


        public static bool UpdateSupplier(UpdateSupplierRequestDto request)
        {
            if (request.SupplierID <= 0 ||
                string.IsNullOrWhiteSpace(request.FullName) ||
                string.IsNullOrWhiteSpace(request.Phone))
                return false;

            var existingSupplier = SupplierRepository.GetSupplierById(request.SupplierID);
            if (existingSupplier == null)
                return false;

            var supplierDTO = new SupplierDto(
                request.SupplierID,
                request.FullName,
                request.Phone,
                request.Address,
                request.Note,
                existingSupplier.IsActive); // لا نسمح بتعديل IsActive هنا

            return SupplierRepository.UpdateSupplier(supplierDTO);
        }



        public static bool DeactivateSupplier(int supplierId)
        {
            if (supplierId <= 0)
                return false;

            return SupplierRepository.DeactivateSupplier(supplierId);

        }

        public static bool ReactivateSupplier(int supplierId)
        {
            if (supplierId <= 0)
                return false;

            return SupplierRepository.ReactivateSupplier(supplierId);
        }
    }
}
