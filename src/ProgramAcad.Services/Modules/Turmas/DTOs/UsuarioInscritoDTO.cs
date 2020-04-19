using System;

namespace ProgramAcad.Services.Modules.Turmas.DTOs
{
    public class UsuarioInscritoDTO
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataInscricao { get; set; }
        public bool? IsAceito { get; set; }
    }
}
