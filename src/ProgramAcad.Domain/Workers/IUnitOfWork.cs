using System.Threading.Tasks;

namespace ProgramAcad.Domain.Workers
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
