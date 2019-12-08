using ProgramAcad.Domain.Contracts.Repositories;
using ProgramAcad.Domain.Entities;
using ProgramAcad.Infra.Data.Workers;

namespace ProgramAcad.Infra.Data.Repository.Contracts
{
    public class TurmaUsuarioRepository : Repository<TurmaUsuario>, ITurmaUsuarioRepository
    {
        public TurmaUsuarioRepository(ProgramAcadDataContext dbContext) : base(dbContext)
        {
        }
    }
}
