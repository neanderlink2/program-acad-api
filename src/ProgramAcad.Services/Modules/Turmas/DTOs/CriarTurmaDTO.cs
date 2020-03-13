using System;

namespace ProgramAcad.Services.Modules.Turmas.DTOs
{
    public class CriarTurmaDTO : BaseTurmaDTO
    {
        public string EmailInstrutor { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
