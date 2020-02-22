using System.Text.Json.Serialization;

namespace ProgramAcad.Services.Modules.Usuarios.DTOs
{
    public class CadastrarUsuarioDTO
    {
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string Senha { get; set; }
        [JsonIgnore]
        public string Role { get; set; } = "ESTUDANTE";
    }
}
