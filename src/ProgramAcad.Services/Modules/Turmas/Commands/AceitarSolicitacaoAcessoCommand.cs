using ProgramAcad.Common.Constants;
using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Domain.Workers;
using ProgramAcad.Services.Modules.Common;
using ProgramAcad.Services.Modules.Turmas.Commands.Validations;
using ProgramAcad.Services.Modules.Turmas.DTOs;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Modules.Turmas.Commands
{
    public class AceitarSolicitacaoAcessoCommand : Command<SolicitarAcessoTurmaDTO>
    {
        private readonly ITurmaUsuarioRepository _turmaUsuarioRepository;
        private readonly AceitarSolicitacaoAcessoValidator _validation;

        public AceitarSolicitacaoAcessoCommand(ITurmaUsuarioRepository turmaUsuarioRepository,
            AceitarSolicitacaoAcessoValidator validation,
            DomainNotificationManager notifyManager, IUnitOfWork unitOfWork) : base(notifyManager, unitOfWork)
        {
            _turmaUsuarioRepository = turmaUsuarioRepository;
            _validation = validation;
        }

        public override async Task<bool> ExecuteAsync(SolicitarAcessoTurmaDTO solicitacao)
        {
            var result = _validation.Validate(solicitacao);
            await NotifyValidationErrorsAsync(result);
            if (_notifyManager.HasNotifications()) return false;
            var turmaUsuario = await _turmaUsuarioRepository
                .GetSingleAsync(x => x.IdTurma == solicitacao.IdTurma && x.Estudante.Email.ToUpper() == solicitacao.EmailUsuario.ToUpper());

            if (turmaUsuario == null)
            {
                await NotifyAsync(NotifyReasons.NOT_FOUND, "Este usuário não solicitou acesso a essa turma.");
                return false;
            }

            if (solicitacao.IsAceito)
            {
                turmaUsuario.ConfirmarInscricao();
                await _turmaUsuarioRepository.UpdateAsync(turmaUsuario);
            }
            else
            {
                await _turmaUsuarioRepository.DeleteAsync(turmaUsuario);
            }

            return await CommitChangesAsync();
        }
    }
}
