using FirebaseAdmin.Auth;
using ProgramAcad.Common.Extensions;
using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Domain.Models;
using ProgramAcad.Domain.Workers;
using ProgramAcad.Services.Modules.Common;
using ProgramAcad.Services.Modules.Usuarios.Commands.Validations;
using ProgramAcad.Services.Modules.Usuarios.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Modules.Usuarios.Commands
{
    public class AtualizarUsuarioCommand : Command<AtualizarUsuarioDTO>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly FirebaseAuth _authService;

        public AtualizarUsuarioCommand(IUsuarioRepository usuarioRepository,
            DomainNotificationManager notifyManager, IUnitOfWork unitOfWork) : base(notifyManager, unitOfWork)
        {
            _usuarioRepository = usuarioRepository;
            _authService = FirebaseAuth.DefaultInstance;
        }

        public override async Task<bool> ExecuteAsync(AtualizarUsuarioDTO usuario)
        {
            var validacao = new AtualizarUsuarioValidator(_usuarioRepository).Validate(usuario);
            await NotifyValidationErrorsAsync(validacao);
            if (_notifyManager.HasNotifications()) return false;

            var userRecord = await _authService.GetUserByEmailAsync(usuario.EmailBuscar);

            var userArgs = new UserRecordArgs
            {
                DisplayName = usuario.NomeCompleto,
                Uid = userRecord.Uid
            };

            var claims = new Dictionary<string, object>();

            if (usuario.Cep.HasValue())
            {
                claims.Add(ProgramAcadClaimTypes.Cep, usuario.Cep);
            }

            if (usuario.Cpf.HasValue())
            {
                claims.Add(ProgramAcadClaimTypes.Cpf, usuario.Cpf);
            }

            if (usuario.DataNascimento.HasValue)
            {
                claims.Add(ProgramAcadClaimTypes.DataNascimento, usuario.DataNascimento?.ToString("yyyy-MM-dd"));
            }

            if (usuario.Sexo.HasValue())
            {
                claims.Add(ProgramAcadClaimTypes.Sexo, usuario.Sexo);
            }

            await _authService.UpdateUserAsync(userArgs);
            await _authService.SetCustomUserClaimsAsync(userRecord.Uid, claims);

            var usuarioEntity = await _usuarioRepository.GetSingleAsync(x => x.Email == usuario.EmailBuscar);
            usuarioEntity.EditUsuario(usuario.NomeCompleto, usuario.Cpf, usuario.Cep, usuario.Sexo, usuario.DataNascimento);
            await _usuarioRepository.UpdateAsync(usuarioEntity);

            return await CommitChangesAsync();
        }
    }
}
