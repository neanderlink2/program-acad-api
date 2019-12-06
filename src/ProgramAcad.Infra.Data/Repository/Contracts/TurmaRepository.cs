using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Domain.Entities;
using ProgramAcad.Infra.Data.Workers;

namespace ProgramAcad.Infra.Data.Repository.Contracts
{
    public class TurmaRepository : Repository<Turma>, ITurmaRepository
    {
        public TurmaRepository(ProgramAcadContext dbContext) : base(dbContext)
        {
        }
    }
}
