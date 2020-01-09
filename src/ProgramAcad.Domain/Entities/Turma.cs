using System;
using System.Collections.Generic;

namespace ProgramAcad.Domain.Entities
{
    public class Turma
    {
        public Turma(Guid idInstrutor, string nome, int capacidadeAlunos, string urlImagemTurma, DateTime dataCriacao, DateTime dataTermino)
        {
            Id = Guid.NewGuid();
            IdInstrutor = idInstrutor;
            Nome = nome;
            CapacidadeAlunos = capacidadeAlunos;
            UrlImagemTurma = urlImagemTurma;
            DataCriacao = dataCriacao;
            DataTermino = dataTermino;

            UsuariosInscritos = new List<TurmaUsuario>();
            AlgoritmosDisponiveis = new List<Algoritmo>();
        }

        public Guid Id { get; protected set; }
        public Guid IdInstrutor { get; protected set; }
        public string Nome { get; protected set; }
        public int CapacidadeAlunos { get; protected set; }
        public string UrlImagemTurma { get; protected set; }
        public DateTime DataCriacao { get; protected set; }
        public DateTime DataTermino { get; protected set; }

        public Usuario Instrutor { get; set; }

        public ICollection<Algoritmo> AlgoritmosDisponiveis { get; set; }
        public ICollection<TurmaUsuario> UsuariosInscritos { get; set; }

        public void AddUsuarioInscrito(Usuario estudante, DateTime dataIngresso)
        {
            UsuariosInscritos.Add(new TurmaUsuario(estudante.Id, Id, dataIngresso, true));
        }

        public void EditTurma(string nome, int capacidadeTurma, string urlImagemTurma)
        {
            Nome = nome;
            CapacidadeAlunos = capacidadeTurma;
            UrlImagemTurma = urlImagemTurma;
        }
    }
}
