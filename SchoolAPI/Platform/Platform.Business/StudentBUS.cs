using Microsoft.Extensions.Configuration;
using Platform.Entity;
using Platform.Repository;

namespace Platform.Business
{
    public class StudentBUS
    {
        public StudentBUS() { }

        public void Save(Student student)
            => new StudentDAO().InsertStudent(student);

        public void RemoveStudent(Student student)
            => new StudentDAO().RemoveStudent(student);

        public Student GetStudentByFullName(string fullName)
        {
            return new StudentDAO().GetStudentByFullName(fullName);
        }

        public Student GetStudentById(long id)
        {
            return new StudentDAO().GetStudentById(id);
        }
    }
}