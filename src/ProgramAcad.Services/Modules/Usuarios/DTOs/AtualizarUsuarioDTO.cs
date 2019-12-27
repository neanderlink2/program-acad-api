using Newtonsoft.Json;
using System;

namespace ProgramAcad.Services.Modules.Usuarios.DTOs
{
    public class AtualizarUsuarioDTO
    {
        [JsonIgnore]
        public string EmailBuscar { get; internal set; }
        public string NomeCompleto { get; set; }
        public string Sexo { get; set; }
        public string Cep { get; set; }
        public string Cpf { get; set; }
        public DateTime? DataNascimento { get; set; }
    }
}
