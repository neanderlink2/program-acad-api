using ProgramAcad.Services.Modules.CasosTeste.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Interfaces.Services
{
    public interface ICasoTesteAppService
    {
        Task<IEnumerable<ExecucaoCasoTesteDTO>> SalvarExecucoesCasoTeste(IEnumerable<ExecucaoCasoTesteDTO> execucaoCasos);
    }
}
