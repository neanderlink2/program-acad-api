using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Workers;
using ProgramAcad.Services.Modules.Common;
using ProgramAcad.Services.Modules.Turmas.DTOs;
using System;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Modules.Turmas.Commands
{
    public class CriarTurmaCommand : Command<CriarTurmaDTO>
    {
        public CriarTurmaCommand(DomainNotificationManager notifyManager, IUnitOfWork unitOfWork) 
            : base(notifyManager, unitOfWork)
        {
        }

        public override Task<bool> ExecuteAsync(CriarTurmaDTO commandModel)
        {
            throw new NotImplementedException();
        }
    }
}
