using AutoMapper;
using ProgramAcad.Common.Constants;
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

        public async Task<bool> AtualizarAlgoritmo(AtualizarAlgoritmoDTO algoritmo)
        {
            return await _atualizarAlgoritmo.Execute(algoritmo);
        }

        public async Task<bool> CriarAlgoritmo(CriarAlgoritmoDTO algoritmo)
        {
            return await _criarAlgoritmo.Execute(algoritmo);
        }

        public async Task<bool> DeletarAlgoritmo(Guid idAlgoritmo)
        {
            return await _deletarAlgoritmo.Execute(idAlgoritmo);
        }

        public async Task<ListarAlgoritmoDTO> ObterAlgoritmoPorId(Guid idAlgoritmo)
        {
            var algoritmo = await _algoritmoRepository.GetSingleAsync(x => x.Id == idAlgoritmo, "LinguagensPermitidas", "NivelDificuldade", "TurmaPertencente");
            if (algoritmo == null)
            {
                await Notify(NotifyReasons.NOT_FOUND, "Algoritmo não foi encontrado.");
                return null;
            }
            return _mapper.Map<ListarAlgoritmoDTO>(algoritmo);
        }

        public async Task<IEnumerable<ListarAlgoritmoDTO>> ObterAlgoritmosPorLinguagem(Guid idTurma, LinguagensProgramacao linguagensProgramacao)
        {
            var algoritmo = await _algoritmoRepository
                .GetManyAsync(x => x.LinguagensPermitidas.Any(x => x.IdLinguagem == linguagensProgramacao),
                    "LinguagensPermitidas", "NivelDificuldade", "TurmaPertencente");
            return _mapper.Map<ICollection<ListarAlgoritmoDTO>>(algoritmo);
        }

        public async Task<IEnumerable<ListarAlgoritmoDTO>> ObterAlgoritmosPorNivelDificuldade(Guid idTurma, int nivel)
        {
            var algoritmo = await _algoritmoRepository
                .GetManyAsync(x => x.IdNivelDificuldade == nivel,
                    "LinguagensPermitidas", "NivelDificuldade", "TurmaPertencente");
            return _mapper.Map<IEnumerable<ListarAlgoritmoDTO>>(algoritmo);
        }

        public async Task<IEnumerable<ListarAlgoritmoDTO>> ObterTodosAlgoritmosPorTurma(Guid idTurma)
        {
            var algoritmos = await _algoritmoRepository.GetManyAsync(x => x.IdTurma == idTurma, "LinguagensPermitidas", "NivelDificuldade", "TurmaPertencente");
            return _mapper.Map<IEnumerable<ListarAlgoritmoDTO>>(algoritmos);
        }

        public Task<IQueryable<KeyValueModel>> ObterLinguagensDisponiveis(Guid idTurma) => _algoritmoRepository.GetLingugagensProgramacaoFilter(idTurma);
        public Task<IQueryable<KeyValueModel>> ObterNiveisDificuldadeDisponiveis(Guid idTurma) => _algoritmoRepository.GetNiveisDificuldadeFilter(idTurma);
    }
}