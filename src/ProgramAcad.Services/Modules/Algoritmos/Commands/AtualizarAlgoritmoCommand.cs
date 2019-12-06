using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Domain.Entities;
using ProgramAcad.Domain.Workers;
using ProgramAcad.Services.Modules.Algoritmos.Commands.Validations;
using ProgramAcad.Services.Modules.Algoritmos.DTOs;
using ProgramAcad.Services.Modules.Common;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Modules.Algoritmos.Commands
{
    public class AtualizarAlgoritmoCommand : Command<AtualizarAlgoritmoDTO>
    {
        private readonly IAlgoritmoRepository _algoritmoRepository;
        private readonly INivelDificuldadeRepository _nivelDificuldadeRepository;
        private readonly ICasoTesteRepository _casoTesteRepository;

        public AtualizarAlgoritmoCommand(IAlgoritmoRepository algoritmoRepository, INivelDificuldadeRepository nivelDificuldadeRepository,
            ICasoTesteRepository casoTesteRepository, DomainNotificationManager notifyManager, IUnitOfWork unitOfWork) : base(notifyManager, unitOfWork)
        {
            _algoritmoRepository = algoritmoRepository;
            _nivelDificuldadeRepository = nivelDificuldadeRepository;
            _casoTesteRepository = casoTesteRepository;
        }

        public override async Task<bool> Execute(AtualizarAlgoritmoDTO algoritmo)
        {
            var validacoes = new AtualizarAlgoritmoValidator(_algoritmoRepository, _nivelDificuldadeRepository).Validate(algoritmo);
            await NotifyValidationErrors(validacoes);

            if (_notifyManager.HasNotifications()) return false;

            var nivelDificuldade = await _nivelDificuldadeRepository.GetSingleAsync(x => x.Nivel == algoritmo.NivelDificuldade);
            var algoritmoEntity = await _algoritmoRepository.GetSingleAsync(x => x.Id == algoritmo.Id, "CasosDeTeste");

            algoritmoEntity.EditAlgoritmo(algoritmoEntity.Titulo, algoritmoEntity.HtmlDescricao, algoritmoEntity.IdNivelDificuldade);

            foreach (var casoTeste in algoritmoEntity.CasosDeTeste)
            {
                await _casoTesteRepository.DeleteAsync(casoTeste);
            }

            foreach (var casoTeste in algoritmo.CasosTeste)
            {
                algoritmoEntity.CasosDeTeste.Add(new CasoTeste(casoTeste.EntradaEsperada, casoTeste.SaidaEsperada,
                    casoTeste.TempoMaximoExecucao, algoritmoEntity.Id));
            }

            await _algoritmoRepository.UpdateAsync(algoritmoEntity);

            return await CommitChanges();
        }
    }
}
