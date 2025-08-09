using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static Platform.Entity.Enum;

namespace SchoolAPI.RequestJson
{
    public class StudentJson
    {
        [Required]
        [JsonPropertyName("Nome Completo")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [JsonPropertyName("Email")]
        public string Email { get; set; }

        [Required]
        [JsonPropertyName("Data de Nascimento")]
        public string DateOfBirth { get; set; }

        [Required]
        [JsonPropertyName("Número de Telefone")]
        public string PhoneNumber { get; set; }

        [Required]
        [JsonPropertyName("Endereço")]
        public string Address { get; set; }

        [Required]
        [JsonPropertyName("Data de Matricula")]
        public string RegistrationDate { get; set; }

        [SwaggerIgnore]
        [JsonPropertyName("Status do Aluno")]
        public string? IsActive { get; set; }
    }
}