using ProgramAcad.Common.Models.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ProgramAcad.Domain.Contracts
{
    public interface IRepository<TModel> where TModel : class
    {
        Task AddAsync(TModel entity);
        Task UpdateAsync(TModel entity);
        Task DeleteAsync(TModel entity);

        IQueryable<TQuery> FromSql<TQuery>(string sql);

        IQueryable<TModel> GetAll();
        Task<IQueryable<TModel>> GetAllAsync();
        Task<IQueryable<TModel>> GetAllAsync(params string[] includes);

        IPagedList<TResultado> GetPagedList<TResultado>(Expression<Func<TModel, TResultado>> selecao,
                                                    Expression<Func<TModel, bool>> condicao = null,
                                                    Func<IQueryable<TModel>, IOrderedQueryable<TModel>> ordenacao = null,
                                                    int indicePagina = 0,
                                                    int tamanhoPagina = 20,
                                                    bool isTracking = false,
                                                    params string[] includes) where TResultado : class;
        Task<IPagedList<TResultado>> GetPagedListAsync<TResultado>(Expression<Func<TModel, TResultado>> selecao,
                                                    Expression<Func<TModel, bool>> condicao = null,
                                                    Func<IQueryable<TModel>, IOrderedQueryable<TModel>> ordenacao = null,
                                                    int indicePagina = 0,
                                                    int tamanhoPagina = 20,
                                                    bool isTracking = false,
                                                    params string[] includes) where TResultado : class;

        Task<IPagedList<TResultado>> GetPagedListAsync<TResultado>(Expression<Func<TModel, TResultado>> selecao,
                                                    Expression<Func<TModel, bool>> condicao = null,                                                    
                                                    Func<IQueryable<TResultado>, IOrderedQueryable<TResultado>> ordenacaoPorSelecao = null,
                                                    int indicePagina = 0,
                                                    int tamanhoPagina = 20,
                                                    bool isTracking = false,
                                                    params string[] includes) where TResultado : class;

        IQueryable<TModel> GetMany(Expression<Func<TModel, bool>> condicao);
        Task<IQueryable<TModel>> GetManyAsync(Expression<Func<TModel, bool>> condicao);
        Task<IQueryable<TModel>> GetManyAsync(Expression<Func<TModel, bool>> condicao, params string[] includes);

        TModel GetSingle(Expression<Func<TModel, bool>> where);
        Task<TModel> GetSingleAsync(Expression<Func<TModel, bool>> condicao);
        Task<TModel> GetSingleAsync(Expression<Func<TModel, bool>> condicao, params string[] includes);

        bool Any(Expression<Func<TModel, bool>> condicao);
        Task<bool> AnyAsync(Expression<Func<TModel, bool>> condicao);

        int Count(Expression<Func<TModel, bool>> condicao = null);
        Task<int> CountAsync(Expression<Func<TModel, bool>> condicao = null);
    }
}