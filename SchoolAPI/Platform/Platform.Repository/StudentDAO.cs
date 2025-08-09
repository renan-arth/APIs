using Microsoft.Extensions.Configuration;
using Platform.Entity;
using Platform.Repository.Base;
using System.Data;
using static Platform.Entity.Enum;

namespace Platform.Repository
{
    public class StudentDAO : BaseDAO
    {
        public StudentDAO() : base() { }

        public void InsertStudent(Student student, IDbTransaction transaction = null)
            => Insert(student, transaction);

        public Student GetStudentByFullName(string fullName)
        {
            var connection = GetConnection();

            var parameters = new Dictionary<string, object>
            {
                { "paramFullName", fullName }
            };

            var command = CreateStoredProcedureCommand("GetStudentByFullName", parameters, connection);
            var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Student
                {
                    Id = reader.GetInt64(reader.GetOrdinal("StudentId")),
                    FullName = reader.GetString(reader.GetOrdinal("FullName")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                    PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                    Address = reader.GetString(reader.GetOrdinal("Address")),
                    RegistrationDate = reader.GetDateTime(reader.GetOrdinal("RegistrationDate")),
                    IsActive = (ActiveStatus)Convert.ToInt32(reader.GetValue(reader.GetOrdinal("IsActive"))),
                };
            }

            return null;
        }

        public Student GetStudentById(long id)
        {
            var connection = GetConnection();

            var parameters = new Dictionary<string, object>
            {
                { "paramStudentId", id }
            };

            var command = CreateStoredProcedureCommand("GetStudentById", parameters, connection);
            var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Student
                {
                    Id = reader.GetInt64(reader.GetOrdinal("StudentId")),
                    FullName = reader.GetString(reader.GetOrdinal("FullName")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                    PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                    Address = reader.GetString(reader.GetOrdinal("Address")),
                    RegistrationDate = reader.GetDateTime(reader.GetOrdinal("RegistrationDate")),
                    CreationDate = reader.GetDateTime(reader.GetOrdinal("CreationDate")),
                    UpdateDate = reader.GetDateTime(reader.GetOrdinal("UpdateDate")),
                    IsActive = (ActiveStatus)Convert.ToInt32(reader.GetValue(reader.GetOrdinal("IsActive"))),
                    DeletionDate = reader.IsDBNull(reader.GetOrdinal("DeletionDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DeletionDate"))
                };
            }

            return null;
        }

        public void RemoveStudent(Student student)
        {
            var connection = GetConnection();

            var parameters = new Dictionary<string, object>
            {
                { "paramStudentId", student.Id },
                { "paramDeletionDate", student.DeletionDate }
            };

            var command = CreateStoredProcedureCommand("RemoveStudent", parameters, connection);
            command.ExecuteReader();
        }
    }
}