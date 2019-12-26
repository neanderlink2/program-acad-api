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
            var list = query.Skip(pageNum <= 1 ? 0 : itemsPerPage * pageNum - 1).Take(itemsPerPage);

            var totalPages = (int)Math.Ceiling((double)(query.Count() / itemsPerPage));
            return new PagedList<T>()
            {
                Page = pageNum,
                Items = list,
                TotalPages = totalPages < 1 ? 1 : totalPages,
                HasNextPage = pageNum < totalPages,
                HasPreviousPage = pageNum > 1
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
