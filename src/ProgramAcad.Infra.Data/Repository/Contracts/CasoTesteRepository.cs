using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Domain.Entities;
using ProgramAcad.Infra.Data.Workers;

namespace ProgramAcad.Infra.Data.Repository.Contracts
{
    public class CasoTesteRepository : Repository<CasoTeste>, ICasoTesteRepository
    {
        public CasoTesteRepository(ProgramAcadContext dbContext) : base(dbContext)
        {
        }
    }
}
