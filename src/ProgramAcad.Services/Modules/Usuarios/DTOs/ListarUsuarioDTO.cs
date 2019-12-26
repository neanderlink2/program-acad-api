using System;

namespace ProgramAcad.Services.Modules.Usuarios.DTOs
{
    public class ListarUsuarioDTO
    {
        public Guid Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }        
        public string Sexo { get; set; }
        public string Cep { get; set; }
        public string Cpf { get; set; }
        public DateTime? DataNascimento { get; set; }
    }
}
