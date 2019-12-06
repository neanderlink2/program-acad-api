using Microsoft.EntityFrameworkCore;
using ProgramAcad.Domain.Exceptions;
using ProgramAcad.Domain.Workers;
using System.Threading.Tasks;

namespace ProgramAcad.Infra.Data.Workers
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dataContext;

        public UnitOfWork(DbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> Commit()
        {
            try
            {
                var result = await _dataContext.SaveChangesAsync();
                return result > 0;
            }
            catch (System.Exception ex)
            {
                throw new SaveChangesFailedException(ex.Source, ex.Message);
            }
        }
    }
}
