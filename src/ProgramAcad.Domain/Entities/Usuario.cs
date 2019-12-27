using System;
using System.Collections.Generic;

namespace ProgramAcad.Domain.Entities
{
    public class Usuario
    {
        public Usuario(string nickname, string email, bool isUsuarioExterno)
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;
            IsAtivo = true;
            Role = "ESTUDANTE";

            Email = email;
            Nickname = nickname;
            IsUsuarioExterno = isUsuarioExterno;
        }

        public Guid Id { get; protected set; }
        public string NomeCompleto { get; protected set; }
        public string Nickname { get; protected set; }
        public string Email { get; protected set; }
        public string Cpf { get; protected set; }
        public string Cep { get; protected set; }
        public string Sexo { get; protected set; }
        public string Role { get; protected set; }
        public DateTime? DataNascimento { get; protected set; }
        public DateTime DataCriacao { get; protected set; }
        public bool IsAtivo { get; protected set; }
        public bool IsUsuarioExterno { get; protected set; }

        public ICollection<AlgoritmoResolvido> AlgoritmosResolvidos { get; set; }
        public ICollection<ExecucaoTeste> TestesExecutados { get; set; }
        public ICollection<TurmaUsuario> TurmasInscritas { get; set; }
        public ICollection<Turma> TurmasCriadas { get; set; }

        public void EditUsuario(string nomeCompleto, string cpf, string cep, string sexo, DateTime? dataNascimento)
        {
            NomeCompleto = nomeCompleto;
            Cpf = cpf;
            Cep = cep;
            Sexo = sexo;
            DataNascimento = dataNascimento;
        }

        public void SetNomeCompleto(string nomeCompleto)
        {
            NomeCompleto = nomeCompleto;
        }

        public void UpdateEmail(string email)
        {
            Email = email;
        }

        public void DesativarUsuario()
        {
            IsAtivo = false;
        }
    }
}
