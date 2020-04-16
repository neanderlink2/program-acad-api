using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Domain.Workers;
using ProgramAcad.Services.Modules.Common;
using ProgramAcad.Services.Modules.Turmas.Commands.Validations;
using ProgramAcad.Services.Modules.Turmas.DTOs;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Modules.Turmas.Commands
{
    public class EditarTurmaCommand : Command<EditarTurmaDTO>
    {
        private readonly TurmaValidator _validation;
        private readonly ITurmaRepository _turmaRepository;

        public EditarTurmaCommand(TurmaValidator validation, ITurmaRepository turmaRepository,
            DomainNotificationManager notifyManager, IUnitOfWork unitOfWork)
            : base(notifyManager, unitOfWork)
        {
            _validation = validation;
            _turmaRepository = turmaRepository;
        }

        public override async Task<bool> ExecuteAsync(EditarTurmaDTO turma)
        {
            var result = _validation.Validate(turma);
            await NotifyValidationErrorsAsync(result);
            if (_notifyManager.HasNotifications()) return false;

            var turmaEntity = await _turmaRepository.GetSingleAsync(x => x.Id == turma.Id);
            turmaEntity.EditTurma(turma.NomeTurma, turma.CapacidadeAlunos, turma.UrlImagem, turma.DataHoraTermino);

            await _turmaRepository.UpdateAsync(turmaEntity);

            return await CommitChangesAsync();
        }
    }
}
