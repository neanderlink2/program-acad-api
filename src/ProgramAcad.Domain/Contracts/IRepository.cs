using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProgramAcad.Domain.Contracts
{
    public interface IRepository<TModel> where TModel : class
    {
        Task AddAsync(TModel entity);
        Task UpdateAsync(TModel entity);
        Task DeleteAsync(TModel entity);        

        IQueryable<TRetorno> FromSql<TRetorno>(string sql);

        IQueryable<TModel> GetAll();
        Task<IQueryable<TModel>> GetAllAsync();

        IQueryable<TModel> GetMany(Expression<Func<TModel, bool>> condicao);
        Task<IQueryable<TModel>> GetManyAsync(Expression<Func<TModel, bool>> condicao);

        TModel GetSingle(Expression<Func<TModel, bool>> where);
        Task<TModel> GetSingleAsync(Expression<Func<TModel, bool>> condicao);

        bool Any(Expression<Func<TModel, bool>> condicao);
        Task<bool> AnyAsync(Expression<Func<TModel, bool>> condicao);

        int Count(Expression<Func<TModel, bool>> condicao = null);
        Task<int> CountAsync(Expression<Func<TModel, bool>> condicao = null);
    }
}
