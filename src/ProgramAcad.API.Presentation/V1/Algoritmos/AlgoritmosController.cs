using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgramAcad.API.Presentation.Controllers;
using ProgramAcad.Common.Models;
using ProgramAcad.Common.Models.PagedList;
using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Models;
using ProgramAcad.Services.Interfaces.Services;
using ProgramAcad.Services.Modules.Algoritmos.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProgramAcad.API.Presentation.V1.Algoritmos
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/algoritmos")]
    public class AlgoritmosController : ApiBaseController
    {
        private readonly IAlgoritmoAppService _algoritmoAppService;
        private readonly ICasoTesteAppService _casoTesteAppService;

        public AlgoritmosController(IAlgoritmoAppService algoritmoAppService, ICasoTesteAppService casoTesteAppService,
            DomainNotificationManager notifyManager) : base(notifyManager)
        {
            _algoritmoAppService = algoritmoAppService;
            _casoTesteAppService = casoTesteAppService;
        }

        [HttpGet("{idAlgoritmo}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ListarAlgoritmoDTO))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(IEnumerable<ExpectedError>))]
        public async Task<IActionResult> GetById(Guid idAlgoritmo)
        {
            var algoritmo = await _algoritmoAppService.ObterAlgoritmoPorIdAsync(idAlgoritmo);
            return Response(algoritmo);
        }

        [HttpGet("turma/{idTurma}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PagedList<ListarAlgoritmoDTO>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(IEnumerable<ExpectedError>))]
        public async Task<IActionResult> GetAllByTurma(Guid idTurma, string busca, int numPagina = 0, int qtdePorPagina = 6,
            ColunasOrdenacaoAlgoritmo colunasOrdenacao = ColunasOrdenacaoAlgoritmo.Nome, string direcaoOrdenacao = "asc")
        {
            var emailUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? "";
            var algoritmos = await _algoritmoAppService.ObterTodosAlgoritmosPorTurmaAsync(idTurma,
                emailUsuario,
                busca,
                numPagina,
                qtdePorPagina,
                colunasOrdenacao,
                direcaoOrdenacao);
            return Response(algoritmos);
        }

        [HttpGet("turma/{idTurma}/nivel/{nivel}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PagedList<ListarAlgoritmoDTO>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(IEnumerable<ExpectedError>))]
        public async Task<IActionResult> GetAllByTurma(int nivel, Guid idTurma, string busca, int numPagina = 0, int qtdePorPagina = 6,
            ColunasOrdenacaoAlgoritmo colunasOrdenacao = ColunasOrdenacaoAlgoritmo.Nome, string direcaoOrdenacao = "asc")
        {
            var emailUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? "";
            var algoritmos = await _algoritmoAppService.ObterAlgoritmosPorNivelDificuldadeAsync(nivel,
                idTurma,
                emailUsuario,
                busca,
                numPagina,
                qtdePorPagina,
                colunasOrdenacao,
                direcaoOrdenacao);
            return Response(algoritmos);
        }

        [Authorize]
        [HttpGet("{idAlgoritmo}/testes")]
        public async Task<IActionResult> GetTestesByAlgoritmo(Guid idAlgoritmo)
        {
            if (User.IsInRole("INSTRUTOR"))
            {
                var response = await _casoTesteAppService.ObterTestesPorAlgoritmo(idAlgoritmo);
                return Response(response);
            }
            return Forbid();
        }

        [HttpGet("turma/{idTurma}/filtro/linguagens")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<KeyValueModel>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(IEnumerable<ExpectedError>))]
        public async Task<IActionResult> GetLinguagensDisponiveisFiltro(Guid idTurma)
        {
            var linguagens = await _algoritmoAppService.ObterLinguagensDisponiveisAsync(idTurma);
            return Response(linguagens);
        }

        [HttpGet("turma/{idTurma}/filtro/dificuldades")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(IEnumerable<KeyValueModel>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(IEnumerable<ExpectedError>))]
        public async Task<IActionResult> GetNiveisDificuldadesFiltro(Guid idTurma)
        {
            var linguagens = await _algoritmoAppService.ObterNiveisDificuldadeDisponiveisAsync(idTurma);
            return Response(linguagens);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(ListarAlgoritmoDTO))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(IEnumerable<ExpectedError>))]
        public async Task<IActionResult> CreateAlgoritmo(CriarAlgoritmoDTO algoritmo)
        {
            var algoritmoCriado = await _algoritmoAppService.CriarAlgoritmoAsync(algoritmo);
            return ResponseCreated($"api/algoritmos/{algoritmoCriado.Id}", algoritmoCriado);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ListarAlgoritmoDTO))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(IEnumerable<ExpectedError>))]
        public async Task<IActionResult> EditAlgoritmo(AtualizarAlgoritmoDTO algoritmo)
        {
            var atualizacao = await _algoritmoAppService.AtualizarAlgoritmoAsync(algoritmo);
            return Response(atualizacao);
        }

        [HttpDelete("{idAlgoritmo}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(IEnumerable<ExpectedError>))]
        public async Task<IActionResult> DeletarAlgoritmo(Guid idAlgoritmo)
        {
            await _algoritmoAppService.DeletarAlgoritmoAsync(idAlgoritmo);
            return ResponseNoContent();
        }
    }
}
