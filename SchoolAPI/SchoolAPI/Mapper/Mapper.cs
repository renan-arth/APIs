using Platform.Entity.DTO;
using SchoolAPI.RequestJson;

namespace SchoolAPI.Mapper
{
    public class Mapper
    {
        public static StudentDTO MapperStudenJsonToDTO(StudentJson studentJson)
        {
            return new StudentDTO
            {
                FullName = studentJson.FullName,
                Email = studentJson.Email,
                DateOfBirth = Convert.ToDateTime(studentJson.DateOfBirth),
                PhoneNumber = studentJson.PhoneNumber,
                Address = studentJson.Address,
                RegistrationDate = Convert.ToDateTime(studentJson.RegistrationDate)
            };
        }
    }
}