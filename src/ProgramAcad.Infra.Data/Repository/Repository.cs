using Dapper;
using Microsoft.EntityFrameworkCore;
using ProgramAcad.Common.Extensions;
using ProgramAcad.Domain.Contracts;
using ProgramAcad.Infra.Data.Workers;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProgramAcad.Infra.Data.Repository
{
    public abstract class Repository<TModel> : IRepository<TModel> where TModel : class
    {
        private readonly DbSet<TModel> _dbSet;
        protected ProgramAcadContext _dataContext;

        protected Repository(ProgramAcadContext dbContext)
        {            
            _dbSet = dbContext.Set<TModel>();
            _dataContext = dbContext;
        }

        public Task AddAsync(TModel entity)
        {
            return Task.Run(() => _dbSet.Add(entity));
        }

        public bool Any(Expression<Func<TModel, bool>> condicao)
        {
            return _dbSet.Any(condicao);
        }

        public Task<bool> AnyAsync(Expression<Func<TModel, bool>> condicao)
        {
            return _dbSet.AnyAsync(condicao);
        }

        public int Count(Expression<Func<TModel, bool>> condicao = null)
        {
            return _dbSet.Count(condicao);
        }

        public Task<int> CountAsync(Expression<Func<TModel, bool>> condicao = null)
        {
            return _dbSet.CountAsync(condicao);
        }

        public Task DeleteAsync(TModel entity)
        {
            return Task.Run(() => _dbSet.Remove(entity));
        }

        public IQueryable<TRetorno> FromSql<TRetorno>(string sql)
        {
            return _dataContext.Database.GetDbConnection().Query<TRetorno>(sql).AsQueryable();
        }

        public IQueryable<TModel> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public Task<IQueryable<TModel>> GetAllAsync()
        {
            return Task.Run(() => _dbSet.AsQueryable());
        }

        public Task<IQueryable<TModel>> GetAllAsync(params string[] includes)
        {
            return Task.Run(() => _dbSet.IncludeMultiple(includes));
        }

        public IQueryable<TModel> GetMany(Expression<Func<TModel, bool>> condicao)
        {
            return _dbSet.Where(condicao);
        }

        public Task<IQueryable<TModel>> GetManyAsync(Expression<Func<TModel, bool>> condicao)
        {
            return Task.Run(() => _dbSet.Where(condicao));
        }

        public Task<IQueryable<TModel>> GetManyAsync(Expression<Func<TModel, bool>> condicao, params string[] includes)
        {
            return Task.Run(() => _dbSet.IncludeMultiple(includes).Where(condicao));
        }

        public TModel GetSingle(Expression<Func<TModel, bool>> where)
        {
            return _dbSet.FirstOrDefault(where);
        }

        public Task<TModel> GetSingleAsync(Expression<Func<TModel, bool>> condicao)
        {
            return _dbSet.FirstOrDefaultAsync(condicao);
        }

        public Task<TModel> GetSingleAsync(Expression<Func<TModel, bool>> condicao, params string[] includes)
        {
            return _dbSet.IncludeMultiple(includes).FirstOrDefaultAsync(condicao);
        }

        public Task UpdateAsync(TModel entity)
        {
            return Task.Run(() => _dbSet.Update(entity));
        }
    }
}
