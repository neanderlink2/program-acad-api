using ProgramAcad.Services.Modules.Compiling.Models;
using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Modules.Compiling.RefitInterfaces
{
    public interface ICompilerApiCall
    {
        [Post("/execute")]
        Task<HttpResponseMessage> Execute([Body]CompilerOptions options);
    }
}
