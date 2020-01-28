using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Domain.Entities;
using ProgramAcad.Infra.Data.Workers;

namespace ProgramAcad.Infra.Data.Repository.Contracts
{
    public class ExecucaoTesteRepository : Repository<ExecucaoTeste>, IExecucaoTesteRepository
    {
        public ExecucaoTesteRepository(ProgramAcadDataContext dbContext) : base(dbContext)
        {
        }
    }
}
