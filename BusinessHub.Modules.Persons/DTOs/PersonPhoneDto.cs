using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.Persons.DTOs
{
    public class PersonPhoneDto
    {
        public int PhoneID { get; set; }
        public int PersonID { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }

        public PersonPhoneDto(int phoneID, int personID, string phoneNumber, DateTime createdAt)
        {
            PhoneID = phoneID;
            PersonID = personID;
            PhoneNumber = phoneNumber;
            CreatedAt = createdAt;
        }
    }
}
