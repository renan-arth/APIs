using Microsoft.IdentityModel.Tokens;
using Platform.Entity;
using Platform.Entity.DTO;
using static Platform.Entity.Enum;

namespace Platform.Business.Transactional
{
    public class StudentTRA
    {
        public static void RegisterStudent(StudentDTO studentDTO)
        {
            try
            {
                ValidateStudent(studentDTO);

                Student student = new Student
                {
                    FullName = studentDTO.FullName,
                    DateOfBirth = studentDTO.DateOfBirth,
                    Email = studentDTO.Email,
                    PhoneNumber = studentDTO.PhoneNumber,
                    Address = studentDTO.Address,
                    RegistrationDate = studentDTO.RegistrationDate,
                    IsActive = ActiveStatus.Pending,
                    CreationDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                };

                new StudentBUS().Save(student);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Student GetStudentByFullName(string fullName)
        {
            try
            {
                if(fullName.IsNullOrEmpty())
                    throw new ArgumentException("Preencha o campo");

                return new StudentBUS().GetStudentByFullName(fullName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void RemoveStudent(long id)
        {
            try
            {
                Student student = new StudentBUS().GetStudentById(id);
                if(student == null)
                    throw new ArgumentException("Estudante não encontrado.");

                student.DeletionDate = DateTime.Now;

                new StudentBUS().RemoveStudent(student);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Student GetStudentById(long id)
        {
            try
            {
                Student student =  new StudentBUS().GetStudentById(id);
                if (student == null)
                    throw new ArgumentException("Estudante não encontrado.");

                return student;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void ValidateStudent(StudentDTO studentDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(studentDTO.FullName))
                    throw new ArgumentException("O nome do estudante é obrigatório.");

                if (string.IsNullOrEmpty(studentDTO.DateOfBirth.ToString()))
                    throw new ArgumentException("A data de nascimento é obrigatória.");

                if (string.IsNullOrEmpty(studentDTO.Email))
                    throw new ArgumentException("O email é obrigatório.");

                if (string.IsNullOrEmpty(studentDTO.PhoneNumber))
                    throw new ArgumentException("O número de telefone é obrigatório.");

                if (string.IsNullOrEmpty(studentDTO.Address))
                    throw new ArgumentException("O endereço é obrigatório.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}