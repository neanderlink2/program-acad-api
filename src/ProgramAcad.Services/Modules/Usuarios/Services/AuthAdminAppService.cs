using AutoMapper;
using FirebaseAdmin.Auth;
using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Services.Interfaces.Services;
using ProgramAcad.Services.Modules.Common;
using ProgramAcad.Services.Modules.Usuarios.Commands;
using ProgramAcad.Services.Modules.Usuarios.DTOs;
using System;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Modules.Usuarios.Services
{
    public class AuthAdminAppService : AppService, IAuthAdminAppService
    {
        private readonly FirebaseAuth _authService;
        private readonly CriarUsuarioExternoCommand _criarUsuarioExterno;
        private readonly CriarUsuarioSenhaCommand _criarUsuarioSenha;
        private readonly AtualizarUsuarioCommand _atualizarUsuario;
        private readonly TrocarSenhaCommand _trocarSenha;
        private readonly AprimorarInstrutorCommand _aprimorarInstrutor;
        private readonly IUsuarioRepository _usuarioRepository;

        public AuthAdminAppService(CriarUsuarioExternoCommand criarUsuarioExterno,
            CriarUsuarioSenhaCommand criarUsuarioSenha,
            AtualizarUsuarioCommand atualizarUsuario,
            TrocarSenhaCommand trocarSenha,
            AprimorarInstrutorCommand aprimorarInstrutor,
            IUsuarioRepository usuarioRepository,
            IMapper mapper, DomainNotificationManager notifyManager) : base(mapper, notifyManager)
        {
            _authService = FirebaseAuth.DefaultInstance;
            _criarUsuarioExterno = criarUsuarioExterno;
            _criarUsuarioSenha = criarUsuarioSenha;
            _atualizarUsuario = atualizarUsuario;
            _trocarSenha = trocarSenha;
            _aprimorarInstrutor = aprimorarInstrutor;
            _usuarioRepository = usuarioRepository;
        }

        public Task ChangeEmailAsync(string emailAntigo, string novoEmail)
        {

            throw new NotImplementedException();
        }

        public async Task ChangePasswordAsync(TrocaSenhaDTO trocaSenha)
        {
            await _trocarSenha.ExecuteAsync(trocaSenha);
        }

        public Task ConfirmEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<ListarUsuarioDTO> RegisterUserExternalAsync(CadastrarUsuarioDTO usuario)
        {
            var success = await _criarUsuarioExterno.ExecuteAsync(usuario);
            if (success)
            {
                var usuarioSaved = await _usuarioRepository.GetSingleAsync(x => x.Email.ToUpper() == usuario.Email.ToUpper());
                return _mapper.Map<ListarUsuarioDTO>(usuarioSaved);
            }

            return default;
        }

        public async Task<ListarUsuarioDTO> RegisterUserPasswordAsync(CadastrarUsuarioDTO usuario)
        {
            var success = await _criarUsuarioSenha.ExecuteAsync(usuario);
            if (success)
            {
                var usuarioSaved = await _usuarioRepository.GetSingleAsync(x => x.Email.ToUpper() == usuario.Email.ToUpper());
                return _mapper.Map<ListarUsuarioDTO>(usuarioSaved);
            }
            return default;
        }

        public async Task UpdateUserAsync(string email, AtualizarUsuarioDTO usuario)
        {
            usuario.EmailBuscar = email;
            await _atualizarUsuario.ExecuteAsync(usuario);
        }

        public async Task<ListarUsuarioDTO> UpgradeToTeacherAccount(Guid idUsuario)
        {
            var success = await _aprimorarInstrutor.ExecuteAsync(idUsuario);
            if (success)
            {
                var usuarioAtualizado = await _usuarioRepository.GetSingleAsync(x => x.Id == idUsuario);
                return _mapper.Map<ListarUsuarioDTO>(usuarioAtualizado);
            }
            return default;
        }
    }
}
