using Platform.Entity.Base;
using static Platform.Entity.Enum;

namespace Platform.Entity
{
    public class Student : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime RegistrationDate { get; set; }
        public ActiveStatus IsActive { get; set; }
    }
}