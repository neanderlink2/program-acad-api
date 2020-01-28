using AutoMapper;
using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Extensions;
using ProgramAcad.Services.Interfaces.Services;
using ProgramAcad.Services.Modules.CasosTeste.Commands;
using ProgramAcad.Services.Modules.CasosTeste.DTOs;
using ProgramAcad.Services.Modules.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Modules.CasosTeste.Services
{
    public class CasoTesteAppService : AppService, ICasoTesteAppService
    {
        private readonly SalvarExecucaoTesteCommand _salvarExecucaoTeste;
        private readonly SalvarAlgoritmoConcluidoCommand _salvarAlgoritmoConcluido;

        public CasoTesteAppService(SalvarExecucaoTesteCommand salvarExecucaoTeste,
            SalvarAlgoritmoConcluidoCommand salvarAlgoritmoConcluido,
            IMapper mapper, DomainNotificationManager notifyManager) : base(mapper, notifyManager)
        {
            _salvarExecucaoTeste = salvarExecucaoTeste;
            _salvarAlgoritmoConcluido = salvarAlgoritmoConcluido;
        }

        public async Task<IEnumerable<ExecucaoCasoTesteDTO>> SalvarExecucoesCasoTeste(IEnumerable<ExecucaoCasoTesteDTO> execucaoCasos)
        {
            var hasAnyError = await SalvarExecucao(execucaoCasos).AnyAsync(x => !x.Sucesso);
            if (!hasAnyError)
            {
                var execucao = execucaoCasos.FirstOrDefault();
                if (execucao != null)
                {
                    await _salvarAlgoritmoConcluido.ExecuteAsync(new SalvarAlgoritmoConcluidoDTO
                    {
                        DataConclusao = DateTime.Now,
                        IdAlgoritmo = execucao.IdAlgoritmo,
                        IdUsuario = execucao.IdUsuario,
                        LinguagemUtilizada = execucao.LinguagemUtilizada.GetLinguagemProgramacaoFromCompiler()
                    });
                }
            }

            return execucaoCasos;
        }

        private async IAsyncEnumerable<ExecucaoCasoTesteDTO> SalvarExecucao(IEnumerable<ExecucaoCasoTesteDTO> execucaoCasos)
        {
            foreach (var execucao in execucaoCasos)
            {
                await _salvarExecucaoTeste.ExecuteAsync(execucao);
                yield return execucao;
            }
        }
    }
}
