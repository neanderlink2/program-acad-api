using System;
using System.Collections.Generic;

namespace ProgramAcad.Domain.Entities
{
    public class Usuario
    {
        public Usuario(string nickname, string email, string cpf)
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;
            IsAtivo = true;
            Role = "ESTUDANTE";
            Email = email;
            Nickname = nickname;
            Cpf = cpf;
        }

        public Guid Id { get; protected set; }
        public string NomeCompleto { get; protected set; }
        public string Nickname { get; protected set; }
        public string Email { get; protected set; }
        public string Cpf { get; protected set; }
        public string Cep { get; protected set; }
        public string Sexo { get; protected set; }
        public string Role { get; protected set; }
        public DateTime DataCriacao { get; protected set; }
        public bool IsAtivo { get; protected set; }

        public ICollection<AlgoritmoResolvido> AlgoritmosResolvidos { get; set; }
        public ICollection<ExecucaoTeste> TestesExecutados { get; set; }
        public ICollection<TurmaUsuario> TurmasInscritas { get; set; }
        public ICollection<Turma> TurmasCriadas { get; set; }

        public virtual void Deactivate()
        {
            IsAtivo = false;
        }
    }
}
