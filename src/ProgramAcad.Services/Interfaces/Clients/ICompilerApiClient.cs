using ProgramAcad.Domain.Entities;
using ProgramAcad.Services.Modules.Compiling.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Interfaces.Clients
{
    public interface ICompilerApiClient
    {
        Task<CompilerResponse> Compile(string code, IEnumerable<string> entradas, int language);
    }
}
