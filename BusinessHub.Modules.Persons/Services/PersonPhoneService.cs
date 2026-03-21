using BusinessHub.Modules.Persons.DTOs;
using BusinessHub.Modules.Persons.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.Persons.Services
{
    public class PersonPhoneService
    {
        public static int AddPersonPhone(PersonPhoneDto personPhone, string currentUser)
        {
            if (personPhone == null || personPhone.PersonID <= 0)
                throw new ArgumentException("Invalid data");

            if (string.IsNullOrWhiteSpace(personPhone.PhoneNumber))
                throw new ArgumentException("Phone is required");

            return PersonPhoneRepository.AddPersonPhone(personPhone, currentUser);
        }

        public static List<PersonPhoneDto> GetPhonesByPersonID(int personID)
        {
            if (personID <= 0)
                throw new ArgumentException("Invalid PersonID");

            return PersonPhoneRepository.GetPhonesByPersonID(personID);
        }

        public static int RemovePersonPhone(int phoneID, string currentUser)
        {
            if (phoneID <= 0)
                throw new ArgumentException("Invalid PhoneID");

            return PersonPhoneRepository.RemovePersonPhone(phoneID, currentUser);
        }
    }
}
