using ProgramAcad.Domain.Entities;
using System;

namespace ProgramAcad.Services.Modules.CasosTeste.DTOs
{
    public class ExecucaoCasoTesteDTO
    {        
        public Guid IdCasoTeste { get; set; }
        public Guid IdAlgoritmo { get; set; }
        public Guid IdUsuario { get; set; }
        public bool Sucesso { get; set; }
        public double? TempoExecucao { get; set; }
        public string LinguagemUtilizada { get; set; }
    }
}
