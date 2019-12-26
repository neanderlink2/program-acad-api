using System.Collections.Generic;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;
using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Domain.Entities;
using ProgramAcad.Domain.Models;
using ProgramAcad.Domain.Workers;
using ProgramAcad.Services.Modules.Common;
using ProgramAcad.Services.Modules.Usuarios.Commands.Validations;
using ProgramAcad.Services.Modules.Usuarios.DTOs;

namespace ProgramAcad.Services.Modules.Usuarios.Commands
{
    public class CriarUsuarioExternoCommand : Command<CadastrarUsuarioDTO>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly FirebaseAuth _authService;

        public CriarUsuarioExternoCommand(IUsuarioRepository usuarioRepository,
            DomainNotificationManager notifyManager, IUnitOfWork unitOfWork)
            : base(notifyManager, unitOfWork)
        {
            _usuarioRepository = usuarioRepository;
            _authService = FirebaseAuth.DefaultInstance;
        }

        public override async Task<bool> ExecuteAsync(CadastrarUsuarioDTO usuario)
        {
            var validacao = new CriarUsuarioValidator(_usuarioRepository).ValidateForExternal(usuario);
            await NotifyValidationErrorsAsync(validacao);
            if (_notifyManager.HasNotifications()) return false;

            var firebaseUser = await _authService.GetUserByEmailAsync(usuario.Email);
            var userRecord = new UserRecordArgs()
            {
                Uid = firebaseUser.Uid,
                Email = firebaseUser.Email,
                EmailVerified = false,
                Disabled = false,
                DisplayName = firebaseUser.DisplayName
            };

            var claims = new Dictionary<string, object> {
                { ProgramAcadClaimTypes.Role, "ESTUDANTE" },
                { ProgramAcadClaimTypes.Nickname, usuario.Nickname }
            };

            await _authService.UpdateUserAsync(userRecord);
            await _authService.SetCustomUserClaimsAsync(userRecord.Uid, claims);

            var usuarioEntity = new Usuario(usuario.Nickname, usuario.Email);
            usuarioEntity.SetNomeCompleto(usuario.NomeCompleto);
            await _usuarioRepository.AddAsync(usuarioEntity);

            return true;
        }
    }
}
