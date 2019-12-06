using Microsoft.AspNetCore.Mvc;
using ProgramAcad.Common.Constants;
using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Entities;
using ProgramAcad.Domain.Exceptions;
using ProgramAcad.Services.Interfaces.Clients;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProgramAcad.API.Presentation.Controllers
{
    [ApiController]
    [Route("api/compiler")]
    [Produces("application/json")]
    public class CompilerController : ApiBaseController
    {
        private readonly ICompilerApiClient _compilerApiClient;

        public CompilerController(DomainNotificationManager notifyManager, ICompilerApiClient compilerApiClient) : base(notifyManager)
        {
            _compilerApiClient = compilerApiClient;
        }

        /// <summary>
        /// Compila o código para C# e retorna a saída.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="inputs"></param>
        /// <returns></returns>
        [HttpPost("csharp")]
        public async Task<IActionResult> CompileCSharp([FromForm]string code, [FromForm]string[] inputs)
        {
            try
            {
                var output = await _compilerApiClient.Compile(code, inputs, LinguagensProgramacao.CSharp);
                return Response(output);
            }
            catch (CompilingFailedException ex)
            {
                await Notify(NotifyReasons.BAD_GATEWAY,
                    $"Houve um problema durante a requisição para compilação do código. Detalhes: {ex.Message}");
            }

            return Response("Failed");
        }

        /// <summary>
        /// Compila o código para Java e retorna a saída.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="inputs"></param>
        /// <returns></returns>
        [HttpPost("java")]
        public async Task<IActionResult> CompileJava([FromForm]string code, [FromForm]string[] inputs)
        {
            try
            {
                var output = await _compilerApiClient.Compile(code, inputs, LinguagensProgramacao.Java);
                return Response(output);
            }
            catch (CompilingFailedException ex)
            {
                await Notify(NotifyReasons.BAD_GATEWAY,
                    $"Houve um problema durante a requisição para compilação do código. Detalhes: {ex.Message}");
            }

            return Response("Failed");
        }

        /// <summary>
        /// Compila o código para Python e retorna a saída.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="inputs"></param>
        /// <returns></returns>
        [HttpPost("python")]
        public async Task<IActionResult> CompilePython([FromForm]string code, [FromForm]string[] inputs)
        {
            try
            {
                var output = await _compilerApiClient.Compile(code, inputs, LinguagensProgramacao.Python);
                return Response(output);
            }
            catch (CompilingFailedException ex)
            {
                await Notify(NotifyReasons.BAD_GATEWAY,
                    $"Houve um problema durante a requisição para compilação do código. Detalhes: {ex.Message}");
            }

            return Response("Failed");
        }

        /// <summary>
        /// Compila o código para C e retorna a saída.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="inputs"></param>
        /// <returns></returns>
        [HttpPost("c")]
        public async Task<IActionResult> CompileC([FromForm]string code, [FromForm]string[] inputs)
        {
            try
            {
                var output = await _compilerApiClient.Compile(code, inputs, LinguagensProgramacao.C);
                return Response(output);
            }
            catch (CompilingFailedException ex)
            {
                await Notify(NotifyReasons.BAD_GATEWAY,
                    $"Houve um problema durante a requisição para compilação do código. Detalhes: {ex.Message}");
            }

            return Response("Failed");
        }
    }
}