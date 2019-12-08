using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Domain.Entities;
using ProgramAcad.Infra.Data.Workers;

namespace ProgramAcad.Infra.Data.Repository.Contracts
{
    public class AlgoritmoResolvidoRepository : Repository<AlgoritmoResolvido>, IAlgoritmoResolvidoRepository
    {
        public AlgoritmoResolvidoRepository(ProgramAcadDataContext dbContext) : base(dbContext)
        {
        }
    }
}
