using AutoMapper;
using ProgramAcad.Common.Models.PagedList;
using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Domain.Entities;
using ProgramAcad.Services.Interfaces.Services;
using ProgramAcad.Services.Modules.Common;
using ProgramAcad.Services.Modules.Turmas.Commands;
using ProgramAcad.Services.Modules.Turmas.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Modules.Turmas.Services
{
    public class TurmaAppService : AppService, ITurmaAppService
    {
        private readonly SolicitarAcessoTurmaCommand _solicitarAcesso;
        private readonly AceitarSolicitacaoAcessoCommand _aceitarSolicitacaoAcesso;
        private readonly CriarTurmaCommand _criarTurma;
        private readonly EditarTurmaCommand _editarTurma;
        private readonly AlternarEstadoTurmaCommand _alternarEstado;
        private readonly ITurmaRepository _turmaRepository;
        private readonly ITurmaUsuarioRepository _turmaUsuarioRepository;

        public TurmaAppService(SolicitarAcessoTurmaCommand solicitarAcesso,
            AceitarSolicitacaoAcessoCommand aceitarSolicitacaoAcesso,
            CriarTurmaCommand criarTurma,
            EditarTurmaCommand editarTurma,
            AlternarEstadoTurmaCommand alternarEstado,
            ITurmaRepository turmaRepository, ITurmaUsuarioRepository turmaUsuarioRepository,
            IMapper mapper, DomainNotificationManager notifyManager)
            : base(mapper, notifyManager)
        {
            _solicitarAcesso = solicitarAcesso;
            _aceitarSolicitacaoAcesso = aceitarSolicitacaoAcesso;
            _criarTurma = criarTurma;
            _editarTurma = editarTurma;
            _alternarEstado = alternarEstado;
            _turmaRepository = turmaRepository;
            _turmaUsuarioRepository = turmaUsuarioRepository;
        }

        public async Task AceitarSolicitacaoAcesso(SolicitarAcessoTurmaDTO acesso)
        {
            await _aceitarSolicitacaoAcesso.ExecuteAsync(acesso);
        }

        public async Task AlternarEstado(Guid idTurma)
        {
            await _alternarEstado.ExecuteAsync(idTurma);
        }

        public async Task CriarTurma(CriarTurmaDTO criarTurma)
        {
            await _criarTurma.ExecuteAsync(criarTurma);
        }

        public async Task EditarTurma(EditarTurmaDTO editarTurma)
        {
            await _editarTurma.ExecuteAsync(editarTurma);
        }

        public async Task<ListarTurmaDTO> GetTurmaById(Guid idTurma)
        {
            var turma = await _turmaRepository.GetSingleAsync(x => x.Id == idTurma, "Instrutor");
            return new ListarTurmaDTO(turma);
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
                    DataHoraTermino = x.DataTermino,
                    UrlImagem = x.UrlImagemTurma,
                    NomeInstrutor = x.Instrutor.NomeCompleto,
                    NomeTurma = x.Nome,
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

        public async Task<IPagedList<ListarTurmaDTO>> GetTurmasPagedByInstrutor(string emailInstrutor, string busca, int pageIndex, int totalItems, TurmaColunasOrdenacao colunaOrdenacao, string direcaoOrdenacao = "asc")
        {
            var term = busca?.ToUpper() ?? "";
            var lista = await _turmaRepository.GetPagedListAsync(
                selecao: x => new ListarTurmaDTO
                {
                    Id = x.Id,
                    CapacidadeAlunos = x.CapacidadeAlunos,
                    DataHoraTermino = x.DataTermino,
                    UrlImagem = x.UrlImagemTurma,
                    NomeInstrutor = x.Instrutor.NomeCompleto,
                    NomeTurma = x.Nome
                },
                condicao: x => x.Instrutor.Email.ToUpper() == emailInstrutor && (
                    string.IsNullOrWhiteSpace(term) ||
                    x.Nome.ToUpper().Contains(term) ||
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
                    DataHoraTermino = x.DataTermino,
                    UrlImagem = x.UrlImagemTurma,
                    NomeInstrutor = x.Instrutor.NomeCompleto,
                    NomeTurma = x.Nome
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
                turma.IsUsuarioInscrito = await _turmaUsuarioRepository.AnyAsync(x => x.Estudante.Email.ToUpper() == emailUsuario.ToUpper() && x.IdTurma == turma.Id && x.Aceito);
                listaAtualizada.Add(turma);
            }
            lista.Items = listaAtualizada;
            return lista;
        }

        public async Task<IEnumerable<UsuarioInscritoDTO>> GetUsuariosInscritosByTurma(Guid idTurma)
        {
            var usuarios = await _turmaUsuarioRepository.GetManyAsync(x => x.IdTurma == idTurma, "Estudante");
            var dtos = usuarios
                .Select(x => new UsuarioInscritoDTO
                {
                    Email = x.Estudante.Email,
                    Nome = x.Estudante.NomeCompleto,
                    DataInscricao = x.DataIngresso,
                    IsAceito = x.Aceito,
                })
                .OrderByDescending(x => x.DataInscricao)
                .ToList();
            return dtos;
        }

        public async Task SolicitarAcesso(SolicitarAcessoTurmaDTO acesso)
        {
            await _solicitarAcesso.ExecuteAsync(acesso);
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
