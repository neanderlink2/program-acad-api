using ProgramAcad.Common.Notifications;
using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Domain.Entities;
using ProgramAcad.Domain.Extensions;
using ProgramAcad.Domain.Workers;
using ProgramAcad.Services.Modules.CasosTeste.DTOs;
using ProgramAcad.Services.Modules.Common;
using System.Threading.Tasks;

namespace ProgramAcad.Services.Modules.CasosTeste.Commands
{
    public class SalvarExecucaoTesteCommand : Command<ExecucaoCasoTesteDTO>
    {
        private readonly IExecucaoTesteRepository _execucaoTesteRepository;

        public SalvarExecucaoTesteCommand(IExecucaoTesteRepository execucaoTesteRepository,
            DomainNotificationManager notifyManager, IUnitOfWork unitOfWork) : base(notifyManager, unitOfWork)
        {
            _execucaoTesteRepository = execucaoTesteRepository;
        }

        public override async Task<bool> ExecuteAsync(ExecucaoCasoTesteDTO execucaoTeste)
        {
            var execucao = new ExecucaoTeste(execucaoTeste.IdCasoTeste, execucaoTeste.IdUsuario, execucaoTeste.LinguagemUtilizada.GetLinguagemProgramacaoFromCompiler().Id, execucaoTeste.Sucesso, execucaoTeste.TempoExecucao.Value);
            await _execucaoTesteRepository.AddAsync(execucao);

            return await CommitChangesAsync();
        }
    }
}
