using Microsoft.AspNetCore.Mvc;
using ProgramAcad.API.Presentation.Controllers;
using ProgramAcad.Common.Constants;
using ProgramAcad.Common.Extensions;
using ProgramAcad.Common.Models;
using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Domain.Entities;
using ProgramAcad.Domain.Exceptions;
using ProgramAcad.Domain.Extensions;
using ProgramAcad.Services.Interfaces.Clients;
using ProgramAcad.Services.Interfaces.Services;
using ProgramAcad.Services.Modules.CasosTeste.DTOs;
using ProgramAcad.Services.Modules.Compiling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProgramAcad.API.Presentation.V1.Compilacao
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/compiler")]
    [Produces("application/json")]
    public class CompilerController : ApiBaseController
    {
        private readonly ICompilerApiClient _compilerApiClient;
        private readonly ICasoTesteAppService _casoTesteAppService;

        public CompilerController(DomainNotificationManager notifyManager, ICompilerApiClient compilerApiClient, ICasoTesteAppService casoTesteAppService) : base(notifyManager)
        {
            _compilerApiClient = compilerApiClient;
            _casoTesteAppService = casoTesteAppService;
        }

        /// <summary>
        /// Executa o código e retorna a saída.
        /// </summary>
        /// <param name="code">Código a ser executado</param>
        /// <param name="inputs">Entradas que o código possa solicitar</param>
        /// <param name="language">Linguagem a ser utilizada (csharp, python, c, java, nodejs)</param>
        /// <returns>Resultado da compilação</returns>
        [HttpPost("{language}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CompilerResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(IEnumerable<ExpectedError>))]
        public async Task<IActionResult> ExecuteCode([FromRoute]string language, [FromForm]string code, [FromForm]string[] inputs)
        {
            try
            {
                var linguagem = language.GetLinguagemProgramacaoFromCompiler();
                var output = await _compilerApiClient.Compile(code, inputs, linguagem);
                return Response(output);
            }
            catch (CompilingFailedException ex)
            {
                await Notify(NotifyReasons.BAD_GATEWAY,
                    $"Houve um problema durante a requisição para compilação do código. Detalhes: {ex.Message}");
            }

            return Response(result: "Failed");
        }

        /// <summary>
        /// Executa o código e executa os testes vinculados ao algoritmo que o usuário submeteu.
        /// </summary>
        /// <param name="idAlgoritmo">Algoritmo para validação</param>
        /// <param name="language">Linguagem a ser utilizada (csharp, python, c, java, nodejs)</param>
        /// <param name="code">Código a ser executado</param>
        /// <param name="algoritmoRepository">Repositório de algoritmos (injeção de dependencia)</param>
        /// <param name="usuarioRepository">Repositório de usuários (injeção de dependencia)</param>
        /// <returns>Algoritmos que obtiveram sucesso</returns>
        [HttpPost("{language}/algoritmo/{idAlgoritmo}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ExecucaoCasoTesteDTO[]))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(IEnumerable<ExpectedError>))]
        public async Task<IActionResult> CompileAndTest([FromRoute]Guid idAlgoritmo, [FromRoute]string language,
            [FromForm]string code, [FromServices]IAlgoritmoRepository algoritmoRepository, [FromServices]IUsuarioRepository usuarioRepository)
        {
            var emailUsuario = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            if (emailUsuario != null)
            {
                var usuario = await usuarioRepository.GetSingleAsync(x => x.Email.ToUpper() == emailUsuario.ToUpper());
                var linguagem = language.GetLinguagemProgramacaoFromCompiler();
                var algoritmo = await algoritmoRepository.GetSingleAsync(x => x.Id == idAlgoritmo, "CasosDeTeste");
                var testes = TestarCasos(code, linguagem, usuario.Id, algoritmo.CasosDeTeste);
                var execucoesSalvas = await _casoTesteAppService.SalvarExecucoesCasoTeste(testes);
                var rota = Url.Content("~/api/v1/account/concluidos/algoritmo");
                return ResponseCreated(rota, execucoesSalvas);
        }

        private IEnumerable<ExecucaoCasoTesteDTO> TestarCasos(string codigo, LinguagensProgramacao linguagem, Guid idUsuario, IEnumerable<CasoTeste> casoTestes)
        {
            foreach (var teste in casoTestes)
            {
                var result = _compilerApiClient.Compile(codigo, teste.EntradaEsperada, linguagem).Result;
                yield return new ExecucaoCasoTesteDTO
                {
                    IdCasoTeste = teste.Id,
                    IdAlgoritmo = teste.IdAlgoritmo,
                    IdUsuario = idUsuario,
                    LinguagemUtilizada = linguagem.GetDescription(),
                    Sucesso = result.HasCompilingError || result.CpuTime >= teste.TempoMaximoDeExecucao || teste.SaidaEsperada.SequenceEqual(result.Output.Split("\n").Where(s => !string.IsNullOrWhiteSpace(s))),
                    TempoExecucao = result.CpuTime
                };
            }
        }
    }
}