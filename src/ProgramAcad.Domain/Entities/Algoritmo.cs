﻿using System;
using System.Collections.Generic;

namespace ProgramAcad.Domain.Entities
{
    public class Algoritmo
    {
        public Algoritmo(Guid idTurma, string titulo, string htmlDescricao, int idNivelDificuldade, DateTime dataCriacao)
        {
            Id = Guid.NewGuid();
            IdTurma = idTurma;
            DataCriacao = dataCriacao;
            Titulo = titulo;
            HtmlDescricao = htmlDescricao;
            IdNivelDificuldade = idNivelDificuldade;
        }

        public Guid Id { get; protected set; }
        public Guid IdTurma { get; set; }
        public DateTime DataCriacao { get; protected set; }
        public string Titulo { get; protected set; }
        public string HtmlDescricao { get; protected set; }
        public int IdNivelDificuldade { get; protected set; }

        public NivelDificuldade NivelDificuldade { get; set; }
        public Turma TurmaPertencente { get; set; }

        public ICollection<CasoTeste> CasosDeTeste { get; set; }
        public ICollection<AlgoritmoLinguagemDisponivel> LinguagensPermitidas { get; set; }
        public ICollection<AlgoritmoResolvido> AlgoritmosResolvidos { get; set; }
    }
}
