using AutoMapper;
using ProgramAcad.Common.Constants;
using ProgramAcad.Common.Extensions;
using ProgramAcad.Common.Models.PagedList;
using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Domain.Entities;
using ProgramAcad.Domain.Models;
using ProgramAcad.Services.Interfaces.Services;
using ProgramAcad.Services.Modules.Algoritmos.Commands;
using ProgramAcad.Services.Modules.Algoritmos.DTOs;
using ProgramAcad.Services.Modules.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Modules.Algoritmos.Services
{
    public class AlgoritmoAppService : AppService, IAlgoritmoAppService
    {
        private readonly IAlgoritmoRepository _algoritmoRepository;
        private readonly IAlgoritmoResolvidoRepository _algoritmoResolvidoRepository;
        private readonly ITurmaUsuarioRepository _turmaUsuarioRepository;
        private readonly CriarAlgoritmoCommand _criarAlgoritmo;
        private readonly AtualizarAlgoritmoCommand _atualizarAlgoritmo;
        private readonly DeletarAlgoritmoCommand _deletarAlgoritmo;

        public AlgoritmoAppService(IAlgoritmoRepository algoritmoRepository, IAlgoritmoResolvidoRepository algoritmoResolvidoRepository,
            ITurmaUsuarioRepository turmaUsuarioRepository,
            CriarAlgoritmoCommand criarAlgoritmoCommand, AtualizarAlgoritmoCommand atualizarAlgoritmoCommand,
            DeletarAlgoritmoCommand deletarAlgoritmoCommand, IMapper mapper, DomainNotificationManager notifyManager) : base(mapper, notifyManager)
        {
            _algoritmoRepository = algoritmoRepository;
            _algoritmoResolvidoRepository = algoritmoResolvidoRepository;
            _turmaUsuarioRepository = turmaUsuarioRepository;
            _criarAlgoritmo = criarAlgoritmoCommand;
            _atualizarAlgoritmo = atualizarAlgoritmoCommand;
            _deletarAlgoritmo = deletarAlgoritmoCommand;
        }

        public async Task<ListarAlgoritmoDTO> AtualizarAlgoritmoAsync(AtualizarAlgoritmoDTO algoritmo)
        {
            await _atualizarAlgoritmo.ExecuteAsync(algoritmo);
            var alg = await _algoritmoRepository.GetSingleAsync(x => x.IsAtivo && x.Id == algoritmo.Id);
            return _mapper.Map<ListarAlgoritmoDTO>(alg);
        }

        public async Task<ListarAlgoritmoDTO> CriarAlgoritmoAsync(CriarAlgoritmoDTO algoritmo)
        {
            await _criarAlgoritmo.ExecuteAsync(algoritmo);
            var alg = await _algoritmoRepository.GetSingleAsync(x => x.IsAtivo && x.IdTurma == algoritmo.IdTurma &&
                x.Titulo.ToUpper() == algoritmo.Titulo.ToUpper() && x.NivelDificuldade.Nivel == algoritmo.NivelDificuldade &&
                x.HtmlDescricao == algoritmo.HtmlDescricao);
            return _mapper.Map<ListarAlgoritmoDTO>(alg);
        }

        public async Task<bool> DeletarAlgoritmoAsync(Guid idAlgoritmo)
        {
            return await _deletarAlgoritmo.ExecuteAsync(idAlgoritmo);
        }

        public async Task<ListarAlgoritmoDTO> ObterAlgoritmoPorIdAsync(Guid idAlgoritmo)
        {
            var algoritmo = await _algoritmoRepository.GetSingleAsync(x => x.IsAtivo && x.Id == idAlgoritmo,
                "LinguagensPermitidas", "NivelDificuldade", "TurmaPertencente");
            if (algoritmo == null)
            {
                await NotifyAsync(NotifyReasons.NOT_FOUND, "Algoritmo não foi encontrado.");
                return null;
            }
            return _mapper.Map<ListarAlgoritmoDTO>(algoritmo);
        }

        public async Task<IPagedList<ListarAlgoritmoDTO>> ObterAlgoritmosPorLinguagemAsync(LinguagensProgramacao linguagemProgramacao, Guid idTurma, string emailUsuario, string busca,
            int numPagina, int qtdePorPagina, ColunasOrdenacaoAlgoritmo colunasOrdenacao, string direcaoOrdenacao)
        {
            var term = busca?.ToUpper() ?? "";
            var algoritmos = await _algoritmoRepository.GetPagedListAsync(
                selecao: x => new ListarAlgoritmoDTO()
                {
                    NivelDificuldade = x.NivelDificuldade.Descricao,
                    HtmlDescricao = x.HtmlDescricao,
                    Id = x.Id,
                    IdNivelDificuldade = x.IdNivelDificuldade,
                    IdTurmaPertencente = x.IdTurma,
                    IsResolvido = false,
                    NomeTurma = x.TurmaPertencente.Nome,
                    Titulo = x.Titulo,
                    LinguagensDisponiveis = x.LinguagensPermitidas.Select(l => l.IdLinguagem.GetDescription())
                },
                condicao: algoritmo => (algoritmo.IsAtivo && algoritmo.IdTurma == idTurma && algoritmo.LinguagensPermitidas.Any(l => l.IdLinguagem == linguagemProgramacao)) &&
                    (string.IsNullOrEmpty(term) || algoritmo.Titulo.ToUpper().Contains(term) || algoritmo.NivelDificuldade.Descricao.ToUpper().Contains(term)),
                ordenacao: x => x.OrderBy(a => a.Titulo),
                indicePagina: numPagina,
                tamanhoPagina: qtdePorPagina,
                isTracking: false,
                includes: new[] { "NivelDificuldade", "TurmaPertencente", "LinguagensPermitidas" });

            List<ListarAlgoritmoDTO> listaAtualizada = new List<ListarAlgoritmoDTO>();

            foreach (var algoritmo in algoritmos.Items)
            {
                algoritmo.IsResolvido = await _algoritmoResolvidoRepository.AnyAsync(x => x.IdAlgoritmo == algoritmo.Id && x.Usuario.Email.ToUpper() == emailUsuario.ToUpper());
                algoritmo.PontosNessaTurma = _turmaUsuarioRepository.GetSingle(x => x.IdTurma == algoritmo.IdTurmaPertencente && x.Estudante.Email.ToUpper() == emailUsuario.ToUpper()).PontosUsuario;
                listaAtualizada.Add(algoritmo);
            }
            algoritmos.Items = listaAtualizada;
            return algoritmos;
        }

        public async Task<IPagedList<ListarAlgoritmoDTO>> ObterAlgoritmosPorNivelDificuldadeAsync(int nivel, Guid idTurma, string emailUsuario, string busca, int numPagina,
            int qtdePorPagina, ColunasOrdenacaoAlgoritmo colunasOrdenacao, string direcaoOrdenacao)
        {
            var term = busca?.ToUpper() ?? "";
            var algoritmos = await _algoritmoRepository.GetPagedListAsync(
                selecao: x => new ListarAlgoritmoDTO()
                {
                    NivelDificuldade = x.NivelDificuldade.Descricao,
                    HtmlDescricao = x.HtmlDescricao,
                    Id = x.Id,
                    IdNivelDificuldade = x.IdNivelDificuldade,
                    IdTurmaPertencente = x.IdTurma,
                    IsResolvido = false,
                    NomeTurma = x.TurmaPertencente.Nome,
                    Titulo = x.Titulo,
                    LinguagensDisponiveis = x.LinguagensPermitidas.Select(l => l.IdLinguagem.GetDescription())
                },
                condicao: algoritmo => (algoritmo.IsAtivo && algoritmo.IdTurma == idTurma && algoritmo.IdNivelDificuldade == nivel) && (string.IsNullOrEmpty(term) || 
                    algoritmo.Titulo.ToUpper().Contains(term)),
                ordenacao: x => x.OrderBy(a => a.Titulo),
                indicePagina: numPagina,
                tamanhoPagina: qtdePorPagina,
                isTracking: false,
                includes: new[] { "NivelDificuldade", "TurmaPertencente", "LinguagensPermitidas" });

            List<ListarAlgoritmoDTO> listaAtualizada = new List<ListarAlgoritmoDTO>();

            foreach (var algoritmo in algoritmos.Items)
            {
                algoritmo.IsResolvido = await _algoritmoResolvidoRepository.AnyAsync(x => x.IdAlgoritmo == algoritmo.Id && x.Usuario.Email.ToUpper() == emailUsuario.ToUpper());
                algoritmo.PontosNessaTurma = _turmaUsuarioRepository.GetSingle(x => x.IdTurma == algoritmo.IdTurmaPertencente && x.Estudante.Email.ToUpper() == emailUsuario.ToUpper()).PontosUsuario;
                listaAtualizada.Add(algoritmo);
            }
            algoritmos.Items = listaAtualizada;
            return algoritmos;
        }

        public async Task<IPagedList<ListarAlgoritmoDTO>> ObterTodosAlgoritmosPorTurmaAsync(Guid idTurma, string emailUsuario, string busca, int numPagina, int qtdePorPagina,
            ColunasOrdenacaoAlgoritmo colunasOrdenacao, string direcaoOrdenacao)
        {
            var term = busca?.ToUpper() ?? "";
            var lista = await _algoritmoRepository.GetPagedListAsync(
                selecao: x => new ListarAlgoritmoDTO()
                {
                    NivelDificuldade = x.NivelDificuldade.Descricao,
                    HtmlDescricao = x.HtmlDescricao,
                    Id = x.Id,
                    IdNivelDificuldade = x.IdNivelDificuldade,
                    IdTurmaPertencente = x.IdTurma,
                    IsResolvido = false,
                    NomeTurma = x.TurmaPertencente.Nome,
                    Titulo = x.Titulo,                    
                    LinguagensDisponiveis = x.LinguagensPermitidas.Select(l => l.IdLinguagem.GetDescription())
                },
                condicao: algoritmo => (algoritmo.IsAtivo && algoritmo.IdTurma == idTurma) && (string.IsNullOrEmpty(term) || algoritmo.Titulo.ToUpper().Contains(term) ||
                    algoritmo.NivelDificuldade.Descricao.ToUpper().Contains(term)),
                ordenacao: lista => OrdenarListaAlgoritmos(lista, colunasOrdenacao, direcaoOrdenacao),
                indicePagina: numPagina,
                tamanhoPagina: qtdePorPagina,
                isTracking: false,
                includes: new[] { "NivelDificuldade", "TurmaPertencente", "LinguagensPermitidas" });

            List<ListarAlgoritmoDTO> listaAtualizada = new List<ListarAlgoritmoDTO>();

            foreach (var algoritmo in lista.Items)
            {
                algoritmo.IsResolvido = await _algoritmoResolvidoRepository.AnyAsync(x => x.IdAlgoritmo == algoritmo.Id && x.Usuario.Email.ToUpper() == emailUsuario.ToUpper());
                algoritmo.PontosNessaTurma = _turmaUsuarioRepository.GetSingle(x => x.IdTurma == algoritmo.IdTurmaPertencente && x.Estudante.Email.ToUpper() == emailUsuario.ToUpper()).PontosUsuario;
                listaAtualizada.Add(algoritmo);
            }
            lista.Items = listaAtualizada;
            return lista;
        }

        public Task<IQueryable<KeyValueModel>> ObterLinguagensDisponiveisAsync(Guid idTurma) => _algoritmoRepository.GetLingugagensProgramacaoFilterAsync(idTurma);
        public Task<IQueryable<KeyValueModel>> ObterNiveisDificuldadeDisponiveisAsync(Guid idTurma) => _algoritmoRepository.GetNiveisDificuldadeFilterAsync(idTurma);

        private IOrderedQueryable<Algoritmo> OrdenarListaAlgoritmos(IQueryable<Algoritmo> source, ColunasOrdenacaoAlgoritmo colunaOrdenacao, string direcaoOrdenacao)
        {
            if (direcaoOrdenacao == "asc")
            {
                if (colunaOrdenacao == ColunasOrdenacaoAlgoritmo.Nome)
                {
                    return source.OrderBy(x => x.Titulo);
                }
                else
                {
                    return source.OrderBy(x => x.DataCriacao);
                }
            }
            else
            {
                if (colunaOrdenacao == ColunasOrdenacaoAlgoritmo.Nome)
                {
                    return source.OrderByDescending(x => x.Titulo);
                }
                else
                {
                    return source.OrderByDescending(x => x.DataCriacao);
                }
            }
        }
    }
}