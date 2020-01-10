using AutoMapper;
using ProgramAcad.Common.Models.PagedList;
using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Domain.Entities;
using ProgramAcad.Services.Interfaces.Services;
using ProgramAcad.Services.Modules.Common;
using ProgramAcad.Services.Modules.Turmas.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Modules.Turmas.Services
{
    public class TurmaAppService : AppService, ITurmaAppService
    {
        private readonly ITurmaRepository _turmaRepository;
        private readonly ITurmaUsuarioRepository _turmaUsuarioRepository;

        public TurmaAppService(ITurmaRepository turmaRepository, ITurmaUsuarioRepository turmaUsuarioRepository,
            IMapper mapper, DomainNotificationManager notifyManager)
            : base(mapper, notifyManager)
        {
            _turmaRepository = turmaRepository;
            _turmaUsuarioRepository = turmaUsuarioRepository;
        }

        public async Task<IPagedList<ListarTurmaDTO>> GetTurmasPaged(string busca, int pageIndex, int totalItems,
            TurmaColunasOrdenacao colunaOrdenacao, string direcaoOrdenacao = "asc")
        {
            var term = busca?.ToUpper() ?? "";
            var lista = await _turmaRepository.GetPagedListAsync(
                selecao: x => new ListarTurmaDTO
                {
                    Id = x.Id,
                    CapacidadeAlunos = x.CapacidadeAlunos,
                    DataTermino = x.DataTermino,
                    ImagemTurma = x.UrlImagemTurma,
                    NomeInstrutor = x.Instrutor.NomeCompleto,
                    Titulo = x.Nome,
                    IsUsuarioInscrito = false
                },
                condicao: x => string.IsNullOrWhiteSpace(term) || (x.Nome.ToUpper().Contains(term) || x.Instrutor.NomeCompleto.ToUpper().Contains(term) ||
                    x.DataTermino.Year.ToString().Contains(term)),
                ordenacao: x => OrdenarListaTurmas(x, colunaOrdenacao, direcaoOrdenacao),
                indicePagina: pageIndex,
                tamanhoPagina: totalItems
            );

            return lista;
        }

        public async Task<IPagedList<ListarTurmaDTO>> GetTurmasPagedByUsuario(string emailUsuario, string busca, int pageIndex, int totalItems,
            TurmaColunasOrdenacao colunaOrdenacao, string direcaoOrdenacao = "asc")
        {
            var term = busca?.ToUpper() ?? "";
            var lista = await _turmaRepository.GetPagedListAsync(
                selecao: x => new ListarTurmaDTO
                {
                    Id = x.Id,
                    CapacidadeAlunos = x.CapacidadeAlunos,
                    DataTermino = x.DataTermino,
                    ImagemTurma = x.UrlImagemTurma,
                    NomeInstrutor = x.Instrutor.NomeCompleto,
                    Titulo = x.Nome
                },
                condicao: x => string.IsNullOrWhiteSpace(term) || (x.Nome.ToUpper().Contains(term) || x.Instrutor.NomeCompleto.ToUpper().Contains(term) ||
                    x.DataTermino.Year.ToString().Contains(term)),
                ordenacao: x => OrdenarListaTurmas(x, colunaOrdenacao, direcaoOrdenacao),
                indicePagina: pageIndex,
                tamanhoPagina: totalItems
            );

            List<ListarTurmaDTO> listaAtualizada = new List<ListarTurmaDTO>();

            foreach (var turma in lista.Items)
            {
                turma.IsUsuarioInscrito = await _turmaUsuarioRepository.AnyAsync(x => x.Estudante.Email.ToUpper() == emailUsuario.ToUpper() && x.IdTurma == turma.Id);
                listaAtualizada.Add(turma);
            }
            lista.Items = listaAtualizada;
            return lista;
        }

        private IOrderedQueryable<Turma> OrdenarListaTurmas(IQueryable<Turma> source, TurmaColunasOrdenacao colunaOrdenacao, string direcaoOrdenacao)
        {
            if (direcaoOrdenacao == "asc")
            {
                if (colunaOrdenacao == TurmaColunasOrdenacao.Nome)
                {
                    return source.OrderBy(x => x.Nome);
                }
                else
                {
                    return source.OrderBy(x => x.DataTermino);
                }
            }
            else
            {
                if (colunaOrdenacao == TurmaColunasOrdenacao.Nome)
                {
                    return source.OrderByDescending(x => x.Nome);
                }
                else
                {
                    return source.OrderByDescending(x => x.DataTermino);
                }
            }
        }
    }
}
