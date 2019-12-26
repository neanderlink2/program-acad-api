using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgramAcad.API.Presentation.Controllers;
using ProgramAcad.Common.Notifications;
using ProgramAcad.Services.Interfaces.Services;
using ProgramAcad.Services.Modules.Usuarios.DTOs;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public async Task<IActionResult> CreateAccount(CadastrarUsuarioDTO usuario)
        {
            if (User.Identity.IsAuthenticated)
            {
                usuario.Email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
                usuario.NomeCompleto = User.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Name).Value;
                var usuarioCriado = await _authAdminAppService.RegisterUserExternalAsync(usuario);
                return Response(usuario);
            }
            var usuarioCriadoSenha = await _authAdminAppService.RegisterUserPasswordAsync(usuario);
            return Response(usuario);
        }
    }
}