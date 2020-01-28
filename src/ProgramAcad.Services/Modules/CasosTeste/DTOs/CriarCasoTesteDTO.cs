using System.Collections.Generic;

namespace ProgramAcad.Services.Modules.CasosTeste.DTOs
{
    public class CriarCasoTesteDTO
    {
        public IEnumerable<string> EntradaEsperada { get; set; }
        public IEnumerable<string> SaidaEsperada { get; set; }
        public int TempoMaximoExecucao { get; set; }
    }
}
