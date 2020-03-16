using ProgramAcad.Domain.Entities;
using System;

namespace ProgramAcad.Services.Modules.Turmas.DTOs
{
    public class ListarTurmaDTO : BaseTurmaDTO
    {
        public ListarTurmaDTO()
        {

        }

        /// <summary>
        /// Transforma a entidade Turma em uma DTO.
        /// </summary>
        /// <param name="turma">Dados da turma</param>
        public ListarTurmaDTO(Turma turma, bool isUsuarioInscrito = false)
        {
            Id = turma.Id;
            DataHoraTermino = turma.DataTermino;
            NomeTurma = turma.Nome;
            NomeInstrutor = turma.Instrutor.NomeCompleto;
            CapacidadeAlunos = turma.CapacidadeAlunos;
            UrlImagem = turma.UrlImagemTurma;
            IsUsuarioInscrito = isUsuarioInscrito;
        }

        public Guid Id { get; set; }
        public string NomeInstrutor { get; set; }
        public bool IsUsuarioInscrito { get; set; }
    }
}
