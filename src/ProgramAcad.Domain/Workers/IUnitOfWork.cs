using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ProgramAcad.Domain.Workers
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
        DbContext Context { get; }
    }
}
