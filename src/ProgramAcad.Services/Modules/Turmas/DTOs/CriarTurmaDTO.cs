using System;

namespace ProgramAcad.Services.Modules.Turmas.DTOs
{
    public class CriarTurmaDTO : BaseTurmaDTO
    {
        public Guid IdInstrutor { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
