using Microsoft.EntityFrameworkCore;
using ProgramAcad.Common.Models.PagedList;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ProgramAcad.Common.Extensions
{
    public static class IQueryableExtensions
    {
        public static IPagedList<T> ToPagedList<T>(this IQueryable<T> query, int itemsPerPage, int pageIndex)
        {
            var totalItems = query.Count();
            var list = query.Skip(pageIndex <= 0 ? 0 : itemsPerPage * pageIndex).Take(itemsPerPage);

            var totalPages = Math.Ceiling((double)totalItems / itemsPerPage);
            return new PagedList<T>()
            {
                PageIndex = pageIndex,
                Items = list.ToList(),
                TotalItems = totalItems,
                TotalPages = totalPages <= 0 ? 1 : (int)totalPages,
                HasNextPage = pageIndex + 1 < totalPages,
                HasPreviousPage = pageIndex > 0
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
