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
    public class PersonRepository
    {
        private static readonly string _cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static int AddPerson(PersonDto person)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_Person_Add", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@FirstName", person.FirstName);

                command.Parameters.AddWithValue("@MiddleName",
                    (object)person.MiddleName ?? DBNull.Value);

                command.Parameters.AddWithValue("@LastName", person.LastName);

                command.Parameters.AddWithValue("@Gender", person.Gender);

                command.Parameters.AddWithValue("@BirthDate",
                    (object)person.BirthDate ?? DBNull.Value);

                command.Parameters.AddWithValue("@NationalNo", person.NationalNo);

                command.Parameters.AddWithValue("@Address",
                    (object)person.Address ?? DBNull.Value);

                command.Parameters.AddWithValue("@Email",
                    (object)person.Email ?? DBNull.Value);

                command.Parameters.AddWithValue("@CreatedBy", person.CreatedBy);

                connection.Open();
                var result = command.ExecuteScalar();

                return result != null ? Convert.ToInt32(result) : 0;
            }
        }


        public static bool UpdatePerson(PersonDto person, string currentUser)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_Person_Update", connection))
            {
                command.CommandType = CommandType.StoredProcedure;


                command.Parameters.AddWithValue("@PersonID", person.PersonID);

                command.Parameters.AddWithValue("@FirstName", person.FirstName);

                command.Parameters.AddWithValue("@MiddleName",
                    (object)person.MiddleName ?? DBNull.Value);

                command.Parameters.AddWithValue("@LastName", person.LastName);

                command.Parameters.AddWithValue("@Gender", person.Gender);

                command.Parameters.AddWithValue("@BirthDate",
                    (object)person.BirthDate ?? DBNull.Value);

                command.Parameters.AddWithValue("@NationalNo", person.NationalNo);

                command.Parameters.AddWithValue("@Address",
                    (object)person.Address ?? DBNull.Value);

                command.Parameters.AddWithValue("@Email",
                    (object)person.Email ?? DBNull.Value);

                command.Parameters.AddWithValue("@UpdatedBy", currentUser);


                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }


        public static List<PersonDto> GetAllPersons()
        {
            var persons = new List<PersonDto>();

            using (SqlConnection conn = new SqlConnection(_cs))
            {


                using (SqlCommand cmd = new SqlCommand("SP_Person_GetAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int personIDIndex = reader.GetOrdinal("PersonID");

                        int firstNameIndex = reader.GetOrdinal("FirstName");
                        int middleNameIndex = reader.GetOrdinal("MiddleName");
                        int lastNameIndex = reader.GetOrdinal("LastName");

                        int genderIndex = reader.GetOrdinal("Gender");

                        int birthDateIndex = reader.GetOrdinal("BirthDate");

                        int nationalNoIndex = reader.GetOrdinal("NationalNo");

                        int addressIndex = reader.GetOrdinal("Address");
                        int emailIndex = reader.GetOrdinal("Email");

                        int createdAtIndex = reader.GetOrdinal("CreatedAt");
                        int createdByIndex = reader.GetOrdinal("CreatedBy");



                        while (reader.Read())
                        {
                            string middleName = reader.IsDBNull(middleNameIndex)
                                ? null
                                : reader.GetString(middleNameIndex);

                            DateTime? birthDate = reader.IsDBNull(birthDateIndex)
                                ? (DateTime?)null
                                : reader.GetDateTime(birthDateIndex);

                            string address = reader.IsDBNull(addressIndex)
                                ? null
                                : reader.GetString(addressIndex);

                            string email = reader.IsDBNull(emailIndex)
                                ? null
                                : reader.GetString(emailIndex);


                            persons.Add(new PersonDto(
                                reader.GetInt32(personIDIndex),
                                reader.GetString(firstNameIndex),
                                middleName,
                                reader.GetString(lastNameIndex),
                                reader.GetBoolean(genderIndex),
                                birthDate,
                                reader.GetString(nationalNoIndex),
                                address,
                                email,
                                reader.GetDateTime(createdAtIndex),
                                reader.GetString(createdByIndex)
                            ));
                        }
                    }
                }
            }

            return persons;
        }


        public static PersonDto GetPersonById(int personID)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_Person_GetByID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PersonID", personID);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    int personIDIndex = reader.GetOrdinal("PersonID");

                    int firstNameIndex = reader.GetOrdinal("FirstName");
                    int middleNameIndex = reader.GetOrdinal("MiddleName");
                    int lastNameIndex = reader.GetOrdinal("LastName");

                    int genderIndex = reader.GetOrdinal("Gender");

                    int birthDateIndex = reader.GetOrdinal("BirthDate");

                    int nationalNoIndex = reader.GetOrdinal("NationalNo");

                    int addressIndex = reader.GetOrdinal("Address");
                    int emailIndex = reader.GetOrdinal("Email");

                    int createdAtIndex = reader.GetOrdinal("CreatedAt");
                    int createdByIndex = reader.GetOrdinal("CreatedBy");

                    if (reader.Read())
                    {
                        string middleName = reader.IsDBNull(middleNameIndex)
                               ? null
                               : reader.GetString(middleNameIndex);

                        DateTime? birthDate = reader.IsDBNull(birthDateIndex)
                            ? (DateTime?)null
                            : reader.GetDateTime(birthDateIndex);

                        string address = reader.IsDBNull(addressIndex)
                            ? null
                            : reader.GetString(addressIndex);

                        string email = reader.IsDBNull(emailIndex)
                            ? null
                            : reader.GetString(emailIndex);

                        return new PersonDto(
                                reader.GetInt32(personIDIndex),
                                reader.GetString(firstNameIndex),
                                middleName,
                                reader.GetString(lastNameIndex),
                                reader.GetBoolean(genderIndex),
                                birthDate,
                                reader.GetString(nationalNoIndex),
                                address,
                                email,
                                reader.GetDateTime(createdAtIndex),
                                reader.GetString(createdByIndex)
                            );
                    }
                    else
                        return null;
                }
            }
        }


        public static PersonDto GetByNationalNo(string nationalNo)
        {
            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_Person_GetByNationalNo", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@NationalNo", nationalNo);

                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    int personIDIndex = reader.GetOrdinal("PersonID");

                    int firstNameIndex = reader.GetOrdinal("FirstName");
                    int middleNameIndex = reader.GetOrdinal("MiddleName");
                    int lastNameIndex = reader.GetOrdinal("LastName");

                    int genderIndex = reader.GetOrdinal("Gender");

                    int birthDateIndex = reader.GetOrdinal("BirthDate");

                    int nationalNoIndex = reader.GetOrdinal("NationalNo");

                    int addressIndex = reader.GetOrdinal("Address");
                    int emailIndex = reader.GetOrdinal("Email");

                    int createdAtIndex = reader.GetOrdinal("CreatedAt");
                    int createdByIndex = reader.GetOrdinal("CreatedBy");

                    if (reader.Read())
                    {
                        string middleName = reader.IsDBNull(middleNameIndex)
                               ? null
                               : reader.GetString(middleNameIndex);

                        DateTime? birthDate = reader.IsDBNull(birthDateIndex)
                            ? (DateTime?)null
                            : reader.GetDateTime(birthDateIndex);

                        string address = reader.IsDBNull(addressIndex)
                            ? null
                            : reader.GetString(addressIndex);

                        string email = reader.IsDBNull(emailIndex)
                            ? null
                            : reader.GetString(emailIndex);

                        return new PersonDto(
                                reader.GetInt32(personIDIndex),
                                reader.GetString(firstNameIndex),
                                middleName,
                                reader.GetString(lastNameIndex),
                                reader.GetBoolean(genderIndex),
                                birthDate,
                                reader.GetString(nationalNoIndex),
                                address,
                                email,
                                reader.GetDateTime(createdAtIndex),
                                reader.GetString(createdByIndex)
                            );
                    }
                    else
                        return null;
                }
            }
        }


        public static List<PersonDto> SearchByLastName(string lastName)
        {

            var persons = new List<PersonDto>();

            using (var connection = new SqlConnection(_cs))
            using (var command = new SqlCommand("SP_Person_SearchByLastName", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LastName", lastName);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    int personIDIndex = reader.GetOrdinal("PersonID");

                    int firstNameIndex = reader.GetOrdinal("FirstName");
                    int middleNameIndex = reader.GetOrdinal("MiddleName");
                    int lastNameIndex = reader.GetOrdinal("LastName");

                    int genderIndex = reader.GetOrdinal("Gender");

                    int birthDateIndex = reader.GetOrdinal("BirthDate");

                    int nationalNoIndex = reader.GetOrdinal("NationalNo");

                    int addressIndex = reader.GetOrdinal("Address");
                    int emailIndex = reader.GetOrdinal("Email");

                    int createdAtIndex = reader.GetOrdinal("CreatedAt");
                    int createdByIndex = reader.GetOrdinal("CreatedBy");



                    while (reader.Read())
                    {
                        string middleName = reader.IsDBNull(middleNameIndex)
                            ? null
                            : reader.GetString(middleNameIndex);

                        DateTime? birthDate = reader.IsDBNull(birthDateIndex)
                            ? (DateTime?)null
                            : reader.GetDateTime(birthDateIndex);

                        string address = reader.IsDBNull(addressIndex)
                            ? null
                            : reader.GetString(addressIndex);

                        string email = reader.IsDBNull(emailIndex)
                            ? null
                            : reader.GetString(emailIndex);


                        persons.Add(new PersonDto(
                            reader.GetInt32(personIDIndex),
                            reader.GetString(firstNameIndex),
                            middleName,
                            reader.GetString(lastNameIndex),
                            reader.GetBoolean(genderIndex),
                            birthDate,
                            reader.GetString(nationalNoIndex),
                            address,
                            email,
                            reader.GetDateTime(createdAtIndex),
                            reader.GetString(createdByIndex)
                        ));
                    }
                }
            }

            return persons;
        }
    }
}
