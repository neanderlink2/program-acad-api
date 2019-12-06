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
    public class CriarAlgoritmoCommand : Command<CriarAlgoritmoDTO>
    {
        private readonly IAlgoritmoRepository _algoritmoRepository;
        private readonly INivelDificuldadeRepository _nivelDificuldadeRepository;
        private readonly ICasoTesteRepository _casoTesteRepository;

        public CriarAlgoritmoCommand(IAlgoritmoRepository algoritmoRepository, INivelDificuldadeRepository nivelDificuldadeRepository,
            ICasoTesteRepository casoTesteRepository, DomainNotificationManager notifyManager, IUnitOfWork unitOfWork) : base(notifyManager, unitOfWork)
        {
            _algoritmoRepository = algoritmoRepository;
            _nivelDificuldadeRepository = nivelDificuldadeRepository;
            _casoTesteRepository = casoTesteRepository;
        }

        public override async Task<bool> Execute(CriarAlgoritmoDTO algoritmo)
        {
            var validacoes = new CriarAlgoritmoValidator(_nivelDificuldadeRepository).Validate(algoritmo);
            await NotifyValidationErrors(validacoes);

            if (_notifyManager.HasNotifications()) return false;

            var nivelDificuldade = await _nivelDificuldadeRepository.GetSingleAsync(x => x.Nivel == algoritmo.NivelDificuldade);

            var entity = new Algoritmo(algoritmo.IdTurma, algoritmo.Titulo, algoritmo.HtmlDescricao, nivelDificuldade.Id, algoritmo.DataCriacao);

            foreach (var casoTeste in algoritmo.CasosTeste)
            {
                entity.CasosDeTeste.Add(new CasoTeste(casoTeste.EntradaEsperada, casoTeste.SaidaEsperada,
                    casoTeste.TempoMaximoExecucao, entity.Id));
            }

            await _algoritmoRepository.AddAsync(entity);

            return await CommitChanges();
        }
    }
}
