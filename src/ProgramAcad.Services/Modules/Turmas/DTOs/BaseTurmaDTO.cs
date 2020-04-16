using System;

namespace ProgramAcad.Services.Modules.Turmas.DTOs
{
    public abstract class BaseTurmaDTO
    {
        public string NomeTurma { get; set; }
        public int CapacidadeAlunos { get; set; }
        public DateTime DataHoraTermino { get; set; }
        public string UrlImagem { get; set; }
    }
}
