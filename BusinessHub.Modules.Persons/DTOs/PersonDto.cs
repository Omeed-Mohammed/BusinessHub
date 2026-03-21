using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.Persons.DTOs
{
    public class PersonDto
    {
        public int PersonID { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public bool Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        public string NationalNo { get; set; }

        public string Address { get; set; }
        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }


        public PersonDto(
                int personID,
                string firstName,
                string middleName,
                string lastName,
                bool gender,
                DateTime? birthDate,
                string nationalNo,
                string address,
                string email,
                DateTime createdAt,
                string createdBy
            )
        {
            PersonID = personID;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Gender = gender;
            BirthDate = birthDate;
            NationalNo = nationalNo;
            Address = address;
            Email = email;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
        }
    }
}
