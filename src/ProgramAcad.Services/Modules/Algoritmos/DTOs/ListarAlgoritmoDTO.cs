﻿using System;
using System.Collections.Generic;

namespace ProgramAcad.Services.Modules.Algoritmos.DTOs
{
    public class ListarAlgoritmoDTO
    {
        public Guid Id { get; set; }
        public Guid IdTurmaPertencente { get; set; }
        public string NomeTurma { get; set; }
        public int PontosNessaTurma { get; set; }
        public string Titulo { get; set; }
        public string HtmlDescricao { get; set; }
        public int IdNivelDificuldade { get; set; }
        public string NivelDificuldade { get; set; }
        public IEnumerable<string> LinguagensDisponiveis { get; set; }
        public bool IsResolvido { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
