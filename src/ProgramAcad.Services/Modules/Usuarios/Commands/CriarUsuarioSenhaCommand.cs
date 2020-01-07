using FirebaseAdmin.Auth;
using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Domain.Entities;
using ProgramAcad.Domain.Models;
using ProgramAcad.Domain.Workers;
using ProgramAcad.Services.Modules.Common;
using ProgramAcad.Services.Modules.Usuarios.Commands.Validations;
using ProgramAcad.Services.Modules.Usuarios.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Modules.Usuarios.Commands
{
    public class CriarUsuarioSenhaCommand : Command<CadastrarUsuarioDTO>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly FirebaseAuth _authService;

        public CriarUsuarioSenhaCommand(IUsuarioRepository usuarioRepository,
            DomainNotificationManager notifyManager, IUnitOfWork unitOfWork)
            : base(notifyManager, unitOfWork)
        {
            _usuarioRepository = usuarioRepository;
            _authService = FirebaseAuth.DefaultInstance;
        }

        public override async Task<bool> ExecuteAsync(CadastrarUsuarioDTO usuario)
        {
            var validacao = new CriarUsuarioValidator(_usuarioRepository).ValidateForPassword(usuario);
            await NotifyValidationErrorsAsync(validacao);
            if (_notifyManager.HasNotifications()) return false;

            var user = new UserRecordArgs()
            {
                Email = usuario.Email,
                EmailVerified = false,
                Disabled = false,
                DisplayName = usuario.NomeCompleto,
                Password = usuario.Senha
            };
            var createdUser = await _authService.CreateUserAsync(user);

            var claims = new Dictionary<string, object> {
                { ProgramAcadClaimTypes.Role, "ESTUDANTE" },
                { ProgramAcadClaimTypes.Nickname, usuario.Nickname }
            };

            await _authService.SetCustomUserClaimsAsync(createdUser.Uid, claims);
            var usuarioEntity = new Usuario(usuario.Nickname, usuario.Email, false);
            usuarioEntity.SetNomeCompleto(usuario.NomeCompleto);
            await _usuarioRepository.AddAsync(usuarioEntity);

            return await CommitChangesAsync();
        }
    }
}
