using Microsoft.AspNetCore.Mvc;
using ProgramAcad.Common.Models;
using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Models;
using ProgramAcad.Services.Interfaces.Services;
using ProgramAcad.Services.Modules.Algoritmos.DTOs;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ProgramAcad.API.Presentation.Controllers
{
    [ApiController]
    [Route("api/algoritmos")]
    public class AlgoritmosController : ApiBaseController
    {
        private readonly IAlgoritmoAppService _algoritmoAppService;

        public AlgoritmosController(IAlgoritmoAppService algoritmoAppService,
            DomainNotificationManager notifyManager) : base(notifyManager)
        {
            _algoritmoAppService = algoritmoAppService;
        }

        [HttpGet("{idAlgoritmo}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Response<ListarAlgoritmoDTO, object>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(Response<object, IEnumerable<ExpectedError>>))]
        public async Task<IActionResult> GetById(Guid idAlgoritmo)
        {
            var algoritmo = await _algoritmoAppService.ObterAlgoritmoPorIdAsync(idAlgoritmo);
            return Response(algoritmo);
        }

        [HttpGet("turma/{idTurma}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Response<IEnumerable<ListarAlgoritmoDTO>, object>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(Response<object, IEnumerable<ExpectedError>>))]
        public async Task<IActionResult> GetAllByTurma(Guid idTurma)
        {
            var algoritmos = await _algoritmoAppService.ObterTodosAlgoritmosPorTurmaAsync(idTurma);
            return Response(algoritmos);
        }

        [HttpGet("turma/{idTurma}/filtro/linguagens")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Response<IEnumerable<KeyValueModel>, object>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(Response<object, IEnumerable<ExpectedError>>))]
        public async Task<IActionResult> GetLinguagensDisponiveisFiltro(Guid idTurma)
        {
            var linguagens = await _algoritmoAppService.ObterLinguagensDisponiveisAsync(idTurma);
            return Response(linguagens);
        }

        [HttpGet("turma/{idTurma}/filtro/dificuldades")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Response<IEnumerable<KeyValueModel>, object>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(Response<object, IEnumerable<ExpectedError>>))]
        public async Task<IActionResult> GetNiveisDificuldadesFiltro(Guid idTurma)
        {
            var linguagens = await _algoritmoAppService.ObterNiveisDificuldadeDisponiveisAsync(idTurma);
            return Response(linguagens);
        }   

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Response<string, object>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(Response<object, IEnumerable<ExpectedError>>))]
        public async Task<IActionResult> CreateAlgoritmo(CriarAlgoritmoDTO algoritmo)
        {
            await _algoritmoAppService.CriarAlgoritmoAsync(algoritmo);
            return Response("Algoritmo criado com sucesso.");
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Response<string, object>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(Response<object, IEnumerable<ExpectedError>>))]
        public async Task<IActionResult> EditAlgoritmo(AtualizarAlgoritmoDTO algoritmo)
        {
            await _algoritmoAppService.AtualizarAlgoritmoAsync(algoritmo);
            return Response("Algoritmo atualizado com sucesso.");
        }

        [HttpDelete("{idAlgoritmo}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Response<string, object>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(Response<object, IEnumerable<ExpectedError>>))]
        public async Task<IActionResult> DeletarAlgoritmo(Guid idAlgoritmo)
        {
            await _algoritmoAppService.DeletarAlgoritmoAsync(idAlgoritmo);
            return Response("Algoritmo foi deletado com sucesso.");
        }
    }
}
