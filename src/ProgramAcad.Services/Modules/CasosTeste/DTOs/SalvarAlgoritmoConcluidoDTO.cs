using ProgramAcad.Domain.Entities;
using System;

namespace ProgramAcad.Services.Modules.CasosTeste.DTOs
{
    public class SalvarAlgoritmoConcluidoDTO
    {
        public Guid IdUsuario { get; set; }
        public Guid IdAlgoritmo { get; set; }
        public LinguagensProgramacao LinguagemUtilizada { get; set; }
        public DateTime DataConclusao { get; set; }
    }
}
