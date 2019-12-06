using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProgramAcad.Common.Extensions;
using ProgramAcad.Common.Models;
using ProgramAcad.Domain.Configurations;
using ProgramAcad.Domain.Entities;
using ProgramAcad.Domain.Exceptions;
using ProgramAcad.Services.Interfaces.Clients;
using ProgramAcad.Services.Modules.Compiling.Models;
using ProgramAcad.Services.Modules.Compiling.RefitInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Modules.Compiling
{
    public class CompilerApiClient : ICompilerApiClient
    {
        private readonly ICompilerApiCall _compilerApiCall;
        private readonly JDoodleConfigs _configs;

        public CompilerApiClient(ICompilerApiCall compilerApiCall, IOptions<JDoodleConfigs> configs)
        {
            _compilerApiCall = compilerApiCall;
            _configs = configs.Value;
        }

        public async Task<CompilerResponse> Compile(string code, IEnumerable<string> entradas, LinguagensProgramacao language)
        {
            var options = CreateCompilerOptions(code, language, entradas.Any() ? entradas.Aggregate((prev, str) => $"{prev}\n{str}") : "");

            var response = await _compilerApiCall.Execute(options);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new CompilingFailedException(JsonConvert.DeserializeObject<ExpectedError>(error));
            }

            return JsonConvert.DeserializeObject<CompilerResponse>(await response.Content.ReadAsStringAsync());
        }

        private CompilerOptions CreateCompilerOptions(string code, LinguagensProgramacao language, string input)
        {
            return new CompilerOptions(_configs.ClientId, _configs.ClientSecret, code, language.GetDescription(),
                language.GetCompilerType(), input);
        }
    }
}
