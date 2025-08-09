using System.ComponentModel.DataAnnotations;

namespace Platform.Entity
{
    public class Enum
    {
        public enum ActiveStatus
        {
            [Display(Name = "Inativo")]
            Inactive = 0,
            [Display(Name = "Pendente")]
            Pending = 1,
            [Display(Name = "Ativo")]
            Active = 2
        }
    }
}