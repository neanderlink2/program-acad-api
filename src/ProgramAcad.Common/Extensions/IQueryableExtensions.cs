using Microsoft.EntityFrameworkCore;
using ProgramAcad.Common.Models.PagedList;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ProgramAcad.Common.Extensions
{
    public static class IQueryableExtensions
    {
        public static IPagedList<T> ToPagedList<T>(this IQueryable<T> query, int itemsPerPage, int pageNum)
        {
            var list = query.Skip(itemsPerPage * pageNum).Take(itemsPerPage);

            if (pageNum <= 0)
            {
                throw new ArgumentOutOfRangeException("Page number must be greater than zero.");
            }

            return new PagedList<T>()
            {
                Page = pageNum,
                Items = list,
                TotalPages = (int)Math.Ceiling((double)(query.Count() / itemsPerPage))
            };
        }

        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes)
            where T : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query,
                        (current, include) => current.Include(include));
            }

            return query;
        }

        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params string[] includes)
            where T : class
        {            
            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }
            return query;
        }
    }
}
