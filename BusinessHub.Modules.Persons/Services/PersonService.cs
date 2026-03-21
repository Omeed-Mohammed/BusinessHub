using BusinessHub.Modules.Persons.DTOs;
using BusinessHub.Modules.Persons.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.Persons.Services
{
    public class PersonService
    {
        public static int AddPerson(PersonDto person)
        {
            if (person == null)
                throw new ArgumentException("Invalid data");

            if (string.IsNullOrWhiteSpace(person.FirstName))
                throw new ArgumentException("FirstName required");

            return PersonRepository.AddPerson(person);
        }

        public static bool UpdatePerson(PersonDto person, string currentUser)
        {
            if (person == null || person.PersonID <= 0)
                throw new ArgumentException("Invalid data");

            return PersonRepository.UpdatePerson(person, currentUser);
        }

        public static List<PersonDto> GetAllPersons()
        {
            return PersonRepository.GetAllPersons();
        }

        public static PersonDto GetPersonById(int personID)
        {
            if (personID <= 0)
                throw new ArgumentException("Invalid PersonID");

            return PersonRepository.GetPersonById(personID);
        }

        public static PersonDto GetByNationalNo(string nationalNo)
        {
            if (string.IsNullOrWhiteSpace(nationalNo))
                throw new ArgumentException("NationalNo required");

            return PersonRepository.GetByNationalNo(nationalNo);
        }

        public static List<PersonDto> SearchByLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("LastName required");

            return PersonRepository.SearchByLastName(lastName);
        }
    }
}
