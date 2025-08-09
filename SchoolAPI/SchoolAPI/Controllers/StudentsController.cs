using Microsoft.AspNetCore.Mvc;
using Platform.Business.Transactional;
using Platform.Entity;
using Platform.Entity.DTO;
using Platform.Entity.Utils;
using SchoolAPI.RequestJson;
using System.ComponentModel;

namespace SchoolAPI.Controllers
{
    [Route("api/[controller]")]
    public class StudentsController : Controller
    {
        [HttpPost("RegisterStudent")]
        public JsonResult RegisterStudent(StudentJson studentJson)
        {
            if (!ModelState.IsValid)
                return Json(studentJson);

            StudentDTO studentDTO = Mapper.Mapper.MapperStudenJsonToDTO(studentJson);

            StudentTRA.RegisterStudent(studentDTO);

            return Json(new
            {
                Success = true,
                Mensagem = "Aluno cadastrado!"
            });
        }

        [HttpGet("GetStudentByFullName")]
        public JsonResult GetStudentByFullName([FromQuery(Name = "Nome do Aluno")] string FullName)
        {
            Student student = new Student();

            student = StudentTRA.GetStudentByFullName(FullName);

            StudentJson studentJson = new StudentJson
            {
                FullName = student.FullName,
                Email = student.Email,
                DateOfBirth = student.DateOfBirth.ToString("dd/MM/yyyy"),
                PhoneNumber = student.PhoneNumber,
                Address = student.Address,
                RegistrationDate = student.RegistrationDate.ToString("dd/MM/yyyy"),
                IsActive = EnumHelper.GetDisplayName(student.IsActive)
            };

            return Json(new
            {
                Success = true,
                Matricula = student.Id,
                Estudante = studentJson
            });
        }

        [HttpPost("DeleteStudent")]
        public JsonResult DeleteStudent([FromQuery(Name = "Matrícula do Aluno")] long id)
        {
            if(id <= 0)
                return Json(new
                {
                    Success = false,
                    Mensagem = "Matrícula inválida!"
                });

            StudentTRA.RemoveStudent(id);

            return Json(new
            {
                Success = true,
                Mensagem = "Aluno removido do sistema!"
            });
        }

        [HttpGet("GetStudentById")]
        public JsonResult GetStudentById([FromQuery(Name = "Matrícula do Aluno")] long id)
        {
            Student student = new Student();

            student = StudentTRA.GetStudentById(id);

            StudentJson studentJson = new StudentJson
            {
                FullName = student.FullName,
                Email = student.Email,
                DateOfBirth = student.DateOfBirth.ToString("dd/MM/yyyy"),
                PhoneNumber = student.PhoneNumber,
                Address = student.Address,
                RegistrationDate = student.RegistrationDate.ToString("dd/MM/yyyy"),
                IsActive = EnumHelper.GetDisplayName(student.IsActive)
            };

            return Json(new
            {
                Success = true,
                Matricula = student.Id,
                Estudante = studentJson
            });
        }
    }
}