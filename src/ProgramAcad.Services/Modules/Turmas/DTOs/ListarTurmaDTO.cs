using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramAcad.Services.Modules.Turmas.DTOs
{
    public class ListarTurmaDTO
    {
        public Guid Id { get; set; }
        public string NomeInstrutor { get; set; }
        public string Titulo { get; set; }
        public string ImagemTurma { get; set; }
        public int CapacidadeAlunos { get; set; }        
        public DateTime DataTermino { get; set; }
        public bool IsUsuarioInscrito { get; set; }
    }
}
