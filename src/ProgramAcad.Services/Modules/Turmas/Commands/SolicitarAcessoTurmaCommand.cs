using ProgramAcad.Common.Constants;
using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Domain.Entities;
using ProgramAcad.Domain.Workers;
using ProgramAcad.Services.Modules.Common;
using ProgramAcad.Services.Modules.Turmas.Commands.Validations;
using ProgramAcad.Services.Modules.Turmas.DTOs;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Modules.Turmas.Commands
{
    public class SolicitarAcessoTurmaCommand : Command<SolicitarAcessoTurmaDTO>
    {
        private readonly ITurmaUsuarioRepository _turmaUsuarioRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly SolicitarAcessoTurmaValidator _validation;

        public SolicitarAcessoTurmaCommand(ITurmaUsuarioRepository turmaUsuarioRepository, IUsuarioRepository usuarioRepository,
            SolicitarAcessoTurmaValidator validation,
            DomainNotificationManager notifyManager, IUnitOfWork unitOfWork) : base(notifyManager, unitOfWork)
        {
            _turmaUsuarioRepository = turmaUsuarioRepository;
            _usuarioRepository = usuarioRepository;
            _validation = validation;
        }

        public override async Task<bool> ExecuteAsync(SolicitarAcessoTurmaDTO solicitacao)
        {
            var result = _validation.Validate(solicitacao);
            await NotifyValidationErrorsAsync(result);

            if (_notifyManager.HasNotifications()) return false;

            var usuario = await _usuarioRepository
                .GetSingleAsync(x => x.Email.ToUpper().Equals(solicitacao.EmailUsuario.ToUpper()));

            if (await _turmaUsuarioRepository.AnyAsync(x => x.IdTurma == solicitacao.IdTurma && x.IdUsuario == usuario.Id))
            {
                await NotifyAsync(NotifyReasons.VALIDATION_ERROR, "Já existe uma solicitação de acesso pendente.");
                return false;
            }

            var turmaUsuario = new TurmaUsuario(usuario.Id, solicitacao.IdTurma, solicitacao.DataIngresso.Value, null);
            await _turmaUsuarioRepository.AddAsync(turmaUsuario);
            return await CommitChangesAsync();
        }
    }
}
