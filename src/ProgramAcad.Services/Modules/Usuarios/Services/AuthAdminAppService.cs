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
        private readonly IUsuarioRepository _usuarioRepository;

        public AuthAdminAppService(CriarUsuarioExternoCommand criarUsuarioExterno,
            CriarUsuarioSenhaCommand criarUsuarioSenha,
            AtualizarUsuarioCommand atualizarUsuario,
            TrocarSenhaCommand trocarSenha,
            IUsuarioRepository usuarioRepository,
            IMapper mapper, DomainNotificationManager notifyManager) : base(mapper, notifyManager)
        {
            _authService = FirebaseAuth.DefaultInstance;
            _criarUsuarioExterno = criarUsuarioExterno;
            _criarUsuarioSenha = criarUsuarioSenha;
            _atualizarUsuario = atualizarUsuario;
            _trocarSenha = trocarSenha;
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
            await _criarUsuarioExterno.ExecuteAsync(usuario);
            var usuarioSaved = await _usuarioRepository.GetSingleAsync(x => x.Email.ToUpper() == usuario.Email.ToUpper());
            return _mapper.Map<ListarUsuarioDTO>(usuarioSaved);
        }

        public async Task<ListarUsuarioDTO> RegisterUserPasswordAsync(CadastrarUsuarioDTO usuario)
        {
            await _criarUsuarioSenha.ExecuteAsync(usuario);
            var usuarioSaved = await _usuarioRepository.GetSingleAsync(x => x.Email.ToUpper() == usuario.Email.ToUpper());
            return _mapper.Map<ListarUsuarioDTO>(usuarioSaved);
        }

        public async Task UpdateUserAsync(string email, AtualizarUsuarioDTO usuario)
        {
            usuario.EmailBuscar = email;
            await _atualizarUsuario.ExecuteAsync(usuario);
        }
    }
}
