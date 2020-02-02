using System;
using System.Collections.Generic;

namespace ProgramAcad.Services.Modules.CasosTeste.DTOs
{
    public class ListarAlgoritmoResolvidoDTO
    {
        public string NomeUsuario { get; set; }
        public string NomeAlgoritmo { get; set; }
        public string DescricaoNivelDificuldade { get; set; }
        public string NomeTurma { get; set; }
        public string LinguagemUtilizada { get; set; }
        public DateTime DataConclusao { get; set; }
        public IEnumerable<ExecucaoCasoTesteDTO> Testes { get; set; }
    }
}
