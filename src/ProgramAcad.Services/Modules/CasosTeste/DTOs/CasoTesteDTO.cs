using System;
using System.Collections.Generic;

namespace ProgramAcad.Services.Modules.CasosTeste.DTOs
{
    public class CasoTesteDTO
    {
        public Guid? Id { get; set; }
        public IEnumerable<string> EntradaEsperada { get; set; }
        public IEnumerable<string> SaidaEsperada { get; set; }
        public int TempoMaximoExecucao { get; set; }
    }
}
