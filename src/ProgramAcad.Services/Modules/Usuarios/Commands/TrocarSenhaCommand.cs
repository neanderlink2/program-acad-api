using FirebaseAdmin.Auth;
using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Domain.Workers;
using ProgramAcad.Services.Modules.Common;
using ProgramAcad.Services.Modules.Usuarios.Commands.Validations;
using ProgramAcad.Services.Modules.Usuarios.DTOs;
using System;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Modules.Usuarios.Commands
{
    public class TrocarSenhaCommand : Command<TrocaSenhaDTO>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly FirebaseAuth _authService;

        public TrocarSenhaCommand(IUsuarioRepository usuarioRepository,
            DomainNotificationManager notifyManager, IUnitOfWork unitOfWork) : base(notifyManager, unitOfWork)
        {
            _usuarioRepository = usuarioRepository;
            _authService = FirebaseAuth.DefaultInstance;
        }

        public override async Task<bool> ExecuteAsync(TrocaSenhaDTO usuarioSenha)
        {
            var validacao = new TrocarSenhaValidator(_usuarioRepository).Validate(usuarioSenha);
            await NotifyValidationErrorsAsync(validacao);
            if (_notifyManager.HasNotifications()) return false;

            if (await _usuarioRepository.AnyAsync(x => x.Email == usuarioSenha.Email && x.IsUsuarioExterno))
            {
                await NotifyAsync("Usuário externo", "Sua conta é de um provedor externo. Não é possível alterar a senha.");
                return false;
            }

            var usuario = await _authService.GetUserByEmailAsync(usuarioSenha.Email);

            var userRecord = new UserRecordArgs
            {
                Uid = usuario.Uid,
                Password = usuarioSenha.SenhaNova
            };

            await _authService.UpdateUserAsync(userRecord);

            return true;
        }
    }
}
