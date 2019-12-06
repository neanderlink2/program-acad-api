using System;
using System.Collections.Generic;

namespace ProgramAcad.Services.Modules.Algoritmos.DTOs
{
    public class ListarAlgoritmoDTO
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string HtmlDescricao { get; set; }
        public int IdNivelDificuldade { get; set; }
        public int NivelDificuldade { get; set; }
        public IEnumerable<string> LinguagensDisponiveis { get; set; }

        public bool IsResolvido { get; protected set; }

        public void SetIsResolvido(bool isResolvido)
        {
            IsResolvido = isResolvido;
        }
    }
}
