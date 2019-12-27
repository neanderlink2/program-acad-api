using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgramAcad.API.Presentation.Controllers;
using ProgramAcad.Common.Models;
using ProgramAcad.Common.Notifications;
using ProgramAcad.Services.Interfaces.Services;
using ProgramAcad.Services.Modules.Usuarios.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProgramAcad.API.Presentation.V1.Autenticacao
{
    [Route("api/account")]
    public class AccountController : ApiBaseController
    {
        private readonly IAuthAdminAppService _authAdminAppService;

        public AccountController(IAuthAdminAppService authAdminAppService, DomainNotificationManager notifyManager)
             : base(notifyManager)
        {
            _authAdminAppService = authAdminAppService;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ListarUsuarioDTO))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(IEnumerable<ExpectedError>))]
        public async Task<IActionResult> CreateAccount(CadastrarUsuarioDTO usuario)
        {
            if (User.Identity.IsAuthenticated)
            {
                usuario.Email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
                usuario.NomeCompleto = User.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Name).Value;
                var usuarioCriado = await _authAdminAppService.RegisterUserExternalAsync(usuario);
                return Response(usuarioCriado);
            }
            var usuarioCriadoSenha = await _authAdminAppService.RegisterUserPasswordAsync(usuario);
            return Response(usuarioCriadoSenha);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(IEnumerable<ExpectedError>))]
        public async Task<IActionResult> UpdateUser(AtualizarUsuarioDTO usuario)
        {
            var email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            await _authAdminAppService.UpdateUserAsync(email, usuario);
            return ResponseNoContent();
        }

        [HttpPatch("password")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(IEnumerable<ExpectedError>))]
        public async Task<IActionResult> ChangePassword(TrocaSenhaDTO trocaSenha)
        {
            await _authAdminAppService.ChangePasswordAsync(trocaSenha);
            return ResponseNoContent();
        }
    }
}