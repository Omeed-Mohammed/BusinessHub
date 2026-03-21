using BusinessHub.Modules.Persons.DTOs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessHub.Modules.Persons.Repositories
{
    public class PersonPhoneRepository
    {
        private static readonly string _cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static int AddPersonPhone(PersonPhoneDto personPhone, string currentUser)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_PersonPhone_Add", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@PersonID", personPhone.PersonID);

                command.Parameters.AddWithValue("@PhoneNumber", personPhone.PhoneNumber);

                command.Parameters.AddWithValue("@CurrentUser", currentUser);


                connection.Open();
                var result = command.ExecuteScalar();

                return Convert.ToInt32(result);
            }
        }

        public static List<PersonPhoneDto> GetPhonesByPersonID(int personID)
        {
            var personPhones = new List<PersonPhoneDto>();

            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_PersonPhone_GetByPersonID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PersonID", personID);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    int phoneIDIndex = reader.GetOrdinal("PhoneID");
                    int personIDIndex = reader.GetOrdinal("PersonID");

                    int phoneNumberIndex = reader.GetOrdinal("PhoneNumber");

                    int CreatedAtIndex = reader.GetOrdinal("CreatedAt");

                    while (reader.Read())
                    {

                        personPhones.Add(new PersonPhoneDto(
                                reader.GetInt32(phoneIDIndex),
                                personID,
                                reader.GetString(phoneNumberIndex),
                                reader.GetDateTime(CreatedAtIndex)
                            ));
                    }

                }
            }

            return personPhones;
        }

        public static int RemovePersonPhone(int phoneID, string currentUser)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_PersonPhone_Remove", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@PhoneID", phoneID);
                command.Parameters.AddWithValue("@CurrentUser", currentUser);

                connection.Open();

                var result = command.ExecuteScalar();

                return Convert.ToInt32(result);
            }
        }
    }
}
