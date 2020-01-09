using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgramAcad.API.Presentation.Controllers;
using ProgramAcad.Common.Notifications;
using ProgramAcad.Services.Interfaces.Services;
using ProgramAcad.Services.Modules.Turmas.DTOs;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProgramAcad.API.Presentation.V1.Turmas
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/turmas")]
    public class TurmasController : ApiBaseController
    {
        private readonly ITurmaAppService _turmaAppService;

        public TurmasController(ITurmaAppService turmaAppService,
            DomainNotificationManager notifyManager) : base(notifyManager)
        {
            _turmaAppService = turmaAppService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetPaged(string busca = "", int pageIndex = 0, int totalItems = 4, TurmaColunasOrdenacao colunaOrdenacao = TurmaColunasOrdenacao.Nome, string direcaoOrdenacao = "asc")
        {
            var email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? "";
            var response = await _turmaAppService.GetTurmasPagedByUsuario(email, busca, pageIndex, totalItems, colunaOrdenacao, direcaoOrdenacao);
            return Response(response);
        }
    }
}
