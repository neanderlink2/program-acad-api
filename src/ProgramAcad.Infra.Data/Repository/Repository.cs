using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ProgramAcad.Common.Extensions;
using ProgramAcad.Common.Models.PagedList;
using ProgramAcad.Domain.Contracts;
using ProgramAcad.Infra.Data.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ProgramAcad.Infra.Data.Repository
{
    public abstract class Repository<TModel> : IRepository<TModel> where TModel : class
    {
        protected readonly DbSet<TModel> _dbSet;
        protected ProgramAcadDataContext _dataContext;

        protected Repository(ProgramAcadDataContext dbContext)
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
            if (condicao == null)
            {
                return _dbSet.Count();
            }
            return _dbSet.Count(condicao);
        }

        public Task<int> CountAsync(Expression<Func<TModel, bool>> condicao = null)
        {
            if (condicao == null)
            {
                return _dbSet.CountAsync();
            }
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

        public IPagedList<TResultado> GetPagedList<TResultado>(Expression<Func<TModel, TResultado>> selecao,
                                                               Expression<Func<TModel, bool>> condicao = null,
                                                               Func<IQueryable<TModel>, IOrderedQueryable<TModel>> ordenacao = null,
                                                               int indicePagina = 0,
                                                               int tamanhoPagina = 20,
                                                               bool isTracking = false,
                                                               params string[] includes) where TResultado : class
        {
            IQueryable<TModel> query = _dbSet;
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }

            if (includes != null && includes.Any())
            {
                query = query.IncludeMultiple(includes);
            }

            if (condicao != null)
            {
                query = query.Where(condicao);
            }

            if (ordenacao != null)
            {
                query = ordenacao(query);
            }
            var result = query.Select(selecao);
            return result.ToPagedList(tamanhoPagina, indicePagina);
        }

        public Task<IPagedList<TResultado>> GetPagedListAsync<TResultado>(Expression<Func<TModel, TResultado>> selecao,
                                                                      Expression<Func<TModel, bool>> condicao = null,
                                                                      Func<IQueryable<TModel>, IOrderedQueryable<TModel>> ordenacao = null,
                                                                      int indicePagina = 0,
                                                                      int tamanhoPagina = 20,
                                                                      bool isTracking = false,
                                                                      params string[] includes) where TResultado : class
        {
            IQueryable<TModel> query = _dbSet;
            if (!isTracking)
            {
                query = query.AsNoTracking();
            }

            if (includes != null && includes.Any())
            {
                query = query.IncludeMultiple(includes);
            }

            if (condicao != null)
            {
                query = query.Where(condicao);
            }

            if (ordenacao != null)
            {
                query = ordenacao(query);
            }
            var result = query.Select(selecao);
            return Task.Run(() => result.ToPagedList(tamanhoPagina, indicePagina));
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
