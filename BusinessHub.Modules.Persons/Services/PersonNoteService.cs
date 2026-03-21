using BusinessHub.Modules.Persons.DTOs;
using BusinessHub.Modules.Persons.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.Persons.Services
{
    public class PersonNoteService
    {
        public static int AddPersonNote(PersonNoteDto personNote, string currentUser)
        {
            // Validation
            if (personNote == null || personNote.PersonID <= 0)
                throw new ArgumentException("Invalid data");

            if (string.IsNullOrWhiteSpace(personNote.Note))
                throw new ArgumentException("Note is required");

            return PersonNoteRepository.AddPersonNote(personNote, currentUser);
        }

        public static List<PersonNoteDto> GetNoteByPersonID(int personID)
        {
            if (personID <= 0)
                throw new ArgumentException("Invalid PersonID");

            return PersonNoteRepository.GetNoteByPersonID(personID);
        }
    }
}
