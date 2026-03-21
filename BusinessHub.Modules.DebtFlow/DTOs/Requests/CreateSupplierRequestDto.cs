using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.DebtFlow.DTOs.Requests
{
    public class CreateSupplierRequestDto
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }

        public CreateSupplierRequestDto(string fullName, string phone, string address, string note)
        {
            FullName = fullName;
            Phone = phone;
            Address = address;
            Note = note;
        }
    }
}
