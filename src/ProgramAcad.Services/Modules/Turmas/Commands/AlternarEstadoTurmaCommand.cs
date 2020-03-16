using ProgramAcad.Common.Constants;
using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Domain.Workers;
using ProgramAcad.Services.Modules.Common;
using System;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Modules.Turmas.Commands
{
    public class AlternarEstadoTurmaCommand : Command<Guid>
    {
        private readonly ITurmaRepository _turmaRepository;

        public AlternarEstadoTurmaCommand(ITurmaRepository turmaRepository,
            DomainNotificationManager notifyManager, IUnitOfWork unitOfWork) : base(notifyManager, unitOfWork)
        {
            _turmaRepository = turmaRepository;
        }

        public override async Task<bool> ExecuteAsync(Guid idTurma)
        {
            var turma = await _turmaRepository.GetSingleAsync(x => x.Id == idTurma);

            if (turma == null)
            {
                await NotifyAsync(NotifyReasons.NOT_FOUND, "Nenhuma turma foi encontrada com este identificador.");
                return false;
            }

            if (turma.Status)
            {
                turma.Desativar();
            }
            else
            {
                turma.Ativar();
            }

            return await CommitChangesAsync();
        }
    }
}
