using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.Persons.DTOs
{
    public class PersonNoteDto
    {
        public int NoteID { get; set; }
        public int PersonID { get; set; }

        public string Note { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public PersonNoteDto(int noteID, int personID, string note, string createdBy, DateTime createdAt)
        {
            NoteID = noteID;
            PersonID = personID;
            Note = note;
            CreatedBy = createdBy;
            CreatedAt = createdAt;
        }
    }
}
