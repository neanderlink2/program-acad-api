using ProgramAcad.Services.Modules.CasosTeste.DTOs;
using System;
using System.Collections.Generic;

namespace ProgramAcad.Services.Modules.Algoritmos.DTOs
{
    public class AtualizarAlgoritmoDTO
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string HtmlDescricao { get; set; }
        public int NivelDificuldade { get; set; }
        public IEnumerable<string> LinguagensPermitidas { get; set; }
        public IEnumerable<CasoTesteDTO> CasosTeste { get; set; }
    }
}
