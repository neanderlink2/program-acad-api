using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Domain.Entities;
using ProgramAcad.Domain.Workers;
using ProgramAcad.Services.Modules.Algoritmos.Commands.Validations;
using ProgramAcad.Services.Modules.Algoritmos.DTOs;
using ProgramAcad.Services.Modules.CasosTeste.DTOs;
using ProgramAcad.Services.Modules.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Modules.Algoritmos.Commands
{
    public class AtualizarAlgoritmoCommand : Command<AtualizarAlgoritmoDTO>
    {
        private readonly IAlgoritmoRepository _algoritmoRepository;
        private readonly INivelDificuldadeRepository _nivelDificuldadeRepository;
        private readonly ICasoTesteRepository _casoTesteRepository;
        private readonly AtualizarAlgoritmoValidator _validation;

        public AtualizarAlgoritmoCommand(IAlgoritmoRepository algoritmoRepository, INivelDificuldadeRepository nivelDificuldadeRepository,
            ICasoTesteRepository casoTesteRepository, AtualizarAlgoritmoValidator validation,
            DomainNotificationManager notifyManager, IUnitOfWork unitOfWork) : base(notifyManager, unitOfWork)
        {
            _algoritmoRepository = algoritmoRepository;
            _nivelDificuldadeRepository = nivelDificuldadeRepository;
            _casoTesteRepository = casoTesteRepository;
            _validation = validation;
        }

        public override async Task<bool> ExecuteAsync(AtualizarAlgoritmoDTO algoritmo)
        {
            var result = _validation.Validate(algoritmo);
            await NotifyValidationErrorsAsync(result);

            if (_notifyManager.HasNotifications()) return false;

            var nivelDificuldade = await _nivelDificuldadeRepository.GetSingleAsync(x => x.Nivel == algoritmo.NivelDificuldade);
            var algoritmoEntity = await _algoritmoRepository.GetSingleAsync(x => x.Id == algoritmo.Id);
            var testesExistentes = await _casoTesteRepository.GetManyAsync(x => x.IdAlgoritmo == algoritmo.Id);

            RemoverLinguagensDisponiveis(algoritmo.Id);
            algoritmoEntity.EditAlgoritmo(algoritmo.Titulo, algoritmo.HtmlDescricao, algoritmo.NivelDificuldade);
            algoritmoEntity.SetLinguagensProgramacao(algoritmo.LinguagensPermitidas);

            await _algoritmoRepository.UpdateAsync(algoritmoEntity);
            await EditarAdicionarCasosTeste(algoritmo.CasosTeste, algoritmo.Id);

            var testesRemovidos = testesExistentes
                .Select(x => x.Id)
                .AsEnumerable()
                .Where(idAlgoritmo =>
                    !algoritmo.CasosTeste.Any(alg => alg.Id == idAlgoritmo)
                )
                .ToList();
            await RemoverCasosTeste(testesRemovidos);

            return await CommitChangesAsync();
        }

        private async Task EditarAdicionarCasosTeste(IEnumerable<CasoTesteDTO> casosTeste, Guid idAlgoritmo)
        {
            foreach (var casoTeste in casosTeste)
            {
                if (casoTeste.Id.HasValue)
                {
                    var testeEntity = await _casoTesteRepository.GetSingleAsync(x => x.Id == casoTeste.Id);
                    testeEntity.Update(casoTeste.EntradaEsperada, casoTeste.SaidaEsperada, casoTeste.TempoMaximoExecucao);
                    await _casoTesteRepository.UpdateAsync(testeEntity);
                }
                else
                {
                    var novoCasoTeste = new CasoTeste(casoTeste.EntradaEsperada, casoTeste.SaidaEsperada, casoTeste.TempoMaximoExecucao, idAlgoritmo);
                    await _casoTesteRepository.AddAsync(novoCasoTeste);
                }
            }
        }

        private async Task RemoverCasosTeste(IEnumerable<Guid> testesRemovidos)
        {
            var casosTeste = await _casoTesteRepository.GetManyAsync(x => testesRemovidos.Contains(x.Id));
            foreach (var casoTeste in casosTeste.ToList())
            {
                await _casoTesteRepository.DeleteAsync(casoTeste);
            }
        }

        private void RemoverLinguagensDisponiveis(Guid idAlgoritmo)
        {
            var dbSet = _unitOfWork.Context.Set<AlgoritmoLinguagemDisponivel>();
            var linguagensDisponiveis = dbSet
                .AsQueryable()
                .Where(x => x.IdAlgoritmo == idAlgoritmo);
            dbSet.RemoveRange(linguagensDisponiveis);
        }
    }
}
