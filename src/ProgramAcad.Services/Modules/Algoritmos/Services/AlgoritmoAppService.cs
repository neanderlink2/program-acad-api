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
using System.Linq;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Modules.Algoritmos.Services
{
    public class AlgoritmoAppService : AppService, IAlgoritmoAppService
    {
        private readonly IAlgoritmoRepository _algoritmoRepository;
        private readonly CriarAlgoritmoCommand _criarAlgoritmo;
        private readonly AtualizarAlgoritmoCommand _atualizarAlgoritmo;
        private readonly DeletarAlgoritmoCommand _deletarAlgoritmo;

        public AlgoritmoAppService(IAlgoritmoRepository algoritmoRepository, CriarAlgoritmoCommand criarAlgoritmoCommand, AtualizarAlgoritmoCommand atualizarAlgoritmoCommand,
            DeletarAlgoritmoCommand deletarAlgoritmoCommand, IMapper mapper, DomainNotificationManager notifyManager) : base(mapper, notifyManager)
        {
            _algoritmoRepository = algoritmoRepository;
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

        public async Task<IPagedList<ListarAlgoritmoDTO>> ObterAlgoritmosPorLinguagemAsync(Guid idTurma, LinguagensProgramacao linguagensProgramacao,
            int numPagina, int qtdePorPagina)
        {
            var algoritmos = await _algoritmoRepository.GetPagedListAsync(
                selecao: x => new ListarAlgoritmoDTO()
                {
                    NivelDificuldade = x.NivelDificuldade.Nivel,
                    HtmlDescricao = x.HtmlDescricao,
                    Id = x.Id,
                    IdNivelDificuldade = x.IdNivelDificuldade,
                    IdTurmaPertencente = x.IdTurma,
                    IsResolvido = false,
                    NomeTurma = x.TurmaPertencente.Nome,
                    Titulo = x.Titulo,
                    LinguagensDisponiveis = x.LinguagensPermitidas.Select(l => l.IdLinguagem.GetDescription())
                },
                condicao: x => x.IsAtivo && x.IdTurma == idTurma && x.LinguagensPermitidas.Any(l => l.IdLinguagem == linguagensProgramacao),
                ordenacao: x => x.OrderBy(a => a.Titulo),
                indicePagina: numPagina,
                tamanhoPagina: qtdePorPagina,
                isTracking: false,
                includes: new[] { "NivelDificuldade", "TurmaPertencente", "LinguagensPermitidas" });

            return algoritmos;
        }

        public async Task<IPagedList<ListarAlgoritmoDTO>> ObterAlgoritmosPorNivelDificuldadeAsync(Guid idTurma, int nivel,
            int numPagina, int qtdePorPagina)
        {
            var algoritmos = await _algoritmoRepository.GetPagedListAsync(
                selecao: x => new ListarAlgoritmoDTO()
                {
                    NivelDificuldade = x.NivelDificuldade.Nivel,
                    HtmlDescricao = x.HtmlDescricao,
                    Id = x.Id,
                    IdNivelDificuldade = x.IdNivelDificuldade,
                    IdTurmaPertencente = x.IdTurma,
                    IsResolvido = false,
                    NomeTurma = x.TurmaPertencente.Nome,
                    Titulo = x.Titulo,
                    LinguagensDisponiveis = x.LinguagensPermitidas.Select(l => l.IdLinguagem.GetDescription())
                },
                condicao: x => x.IsAtivo && x.IdTurma == idTurma && x.IdNivelDificuldade == nivel,
                ordenacao: x => x.OrderBy(a => a.Titulo),
                indicePagina: numPagina,
                tamanhoPagina: qtdePorPagina,
                isTracking: false,
                includes: new[] { "NivelDificuldade", "TurmaPertencente", "LinguagensPermitidas" });

            return algoritmos;
        }

        public async Task<IPagedList<ListarAlgoritmoDTO>> ObterTodosAlgoritmosPorTurmaAsync(Guid idTurma,
            int numPagina, int qtdePorPagina)
        {
            var algoritmos = await _algoritmoRepository.GetPagedListAsync(
                selecao: x => new ListarAlgoritmoDTO()
                {
                    NivelDificuldade = x.NivelDificuldade.Nivel,
                    HtmlDescricao = x.HtmlDescricao,
                    Id = x.Id,
                    IdNivelDificuldade = x.IdNivelDificuldade,
                    IdTurmaPertencente = x.IdTurma,
                    IsResolvido = false,
                    NomeTurma = x.TurmaPertencente.Nome,
                    Titulo = x.Titulo,
                    LinguagensDisponiveis = x.LinguagensPermitidas.Select(l => l.IdLinguagem.GetDescription())
                },
                condicao: x => x.IsAtivo && x.IdTurma == idTurma,
                ordenacao: x => x.OrderBy(a => a.Titulo),
                indicePagina: numPagina,
                tamanhoPagina: qtdePorPagina,
                isTracking: false,
                includes: new[] { "NivelDificuldade", "TurmaPertencente", "LinguagensPermitidas" });

            return algoritmos;
        }

        public Task<IQueryable<KeyValueModel>> ObterLinguagensDisponiveisAsync(Guid idTurma) => _algoritmoRepository.GetLingugagensProgramacaoFilterAsync(idTurma);
        public Task<IQueryable<KeyValueModel>> ObterNiveisDificuldadeDisponiveisAsync(Guid idTurma) => _algoritmoRepository.GetNiveisDificuldadeFilterAsync(idTurma);
    }
}