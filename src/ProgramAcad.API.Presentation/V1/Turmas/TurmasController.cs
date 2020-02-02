using Microsoft.AspNetCore.Mvc;
using ProgramAcad.API.Presentation.Controllers;
using ProgramAcad.Common.Models;
using ProgramAcad.Common.Models.PagedList;
using ProgramAcad.Common.Notifications;
using ProgramAcad.Services.Interfaces.Services;
using ProgramAcad.Services.Modules.Turmas.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PagedList<ListarTurmaDTO>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(IEnumerable<ExpectedError>))]
        public async Task<IActionResult> GetPaged(string busca = "", int pageIndex = 0, int totalItems = 4, TurmaColunasOrdenacao colunaOrdenacao = TurmaColunasOrdenacao.Nome, string direcaoOrdenacao = "asc")
        {
            var email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? "";
            var response = await _turmaAppService.GetTurmasPagedByUsuario(email, busca, pageIndex, totalItems, colunaOrdenacao, direcaoOrdenacao);
            return Response(response);
        }

        [HttpPost("{idTurma}/acesso")]
        public async Task<IActionResult> SolicitarAcesso(Guid idTurma, [FromQuery]DateTime dataEnvio)
        {
            var email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? "";
            await _turmaAppService.SolicitarAcesso(new SolicitarAcessoTurmaDTO()
            {
                EmailUsuario = email,
                DataIngresso = dataEnvio,
                IdTurma = idTurma
            });
            return ResponseNoContent();
        }

        [HttpPut("{idTurma}/acesso")]
        public async Task<IActionResult> SolicitarAcesso(Guid idTurma, [FromQuery]string emailUsuario)
        {
            await _turmaAppService.AceitarSolicitacaoAcesso(new SolicitarAcessoTurmaDTO()
            {
                EmailUsuario = emailUsuario,
                IdTurma = idTurma
            });
            return ResponseNoContent();
        }
    }
}
