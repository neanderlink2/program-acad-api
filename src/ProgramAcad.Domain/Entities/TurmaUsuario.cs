using System;

namespace ProgramAcad.Domain.Entities
{
    public class TurmaUsuario
    {
        public TurmaUsuario(Guid idUsuario, Guid idTurma, DateTime dataIngresso, bool aceito)
        {
            IdUsuario = idUsuario;
            IdTurma = idTurma;            
            DataIngresso = dataIngresso;
            Aceito = aceito;

            PontosUsuario = 0;
        }

        public Guid IdUsuario { get; protected set; }
        public Guid IdTurma { get; protected set; }
        public int PontosUsuario { get; protected set; }
        public DateTime DataIngresso { get; protected set; }
        public bool Aceito { get; protected set; }

        public Usuario Estudante { get; set; }
        public Turma Turma { get; set; }

        public void AddPontos(int pontos)
        {
            PontosUsuario += pontos;
        }
    }
}
