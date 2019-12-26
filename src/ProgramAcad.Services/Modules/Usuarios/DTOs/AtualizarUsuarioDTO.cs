using System;

namespace ProgramAcad.Services.Modules.Usuarios.DTOs
{
    public class AtualizarUsuarioDTO
    {
        public string NomeCompleto { get; set; }
        public string Sexo { get; set; }
        public string Cep { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
