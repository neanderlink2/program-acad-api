using FirebaseAdmin.Auth;
using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Domain.Models;
using ProgramAcad.Domain.Workers;
using ProgramAcad.Services.Modules.Common;
using ProgramAcad.Services.Modules.Usuarios.Commands.Validations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Modules.Usuarios.Commands
{
    public class AprimorarInstrutorCommand : Command<Guid>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly AprimorarInstrutorValidator _validation;
        private readonly FirebaseAuth _authService;

        public AprimorarInstrutorCommand(IUsuarioRepository usuarioRepository, AprimorarInstrutorValidator aprimorarInstrutorValidator,
            DomainNotificationManager notifyManager, IUnitOfWork unitOfWork) : base(notifyManager, unitOfWork)
        {
            _usuarioRepository = usuarioRepository;
            _validation = aprimorarInstrutorValidator;
            _authService = FirebaseAuth.DefaultInstance;
        }

        public override async Task<bool> ExecuteAsync(Guid idUsuario)
        {
            var validacao = _validation.Validate(idUsuario);
            await NotifyValidationErrorsAsync(validacao);
            if (_notifyManager.HasNotifications()) return false;

            var usuario = await _usuarioRepository.GetSingleAsync(x => x.Id == idUsuario);

            var userRecord = await _authService.GetUserByEmailAsync(usuario.Email);
            var claims = new Dictionary<string, object>(userRecord.CustomClaims)
            {
                [ProgramAcadClaimTypes.Role] = "INSTRUTOR"
            };
            usuario.UpdateRole("INSTRUTOR");

            await _authService.SetCustomUserClaimsAsync(userRecord.Uid, claims);
            await _usuarioRepository.UpdateAsync(usuario);

            return await CommitChangesAsync();
        }
    }
}
