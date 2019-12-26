using Microsoft.EntityFrameworkCore;
using ProgramAcad.Common.Models.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ProgramAcad.Common.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// The SQL's LIKE function.        
        /// </summary>
        /// <typeparam name="T">Type of IQueryable</typeparam>
        /// <param name="queryable">Collection used to query</param>
        /// <param name="property">Property used to query. Must be a String.</param>
        /// <param name="searchedText">Other String that will be used to query the property</param>
        /// <returns>Collection with objects that match the Query</returns>
        public static IEnumerable<T> Like<T>(this IEnumerable<T> queryable, Expression<Func<T, string>> property, string searchedText)
        {
            var getPropertyValue = property.Compile();
            var query = queryable.Where(x => EF.Functions.Like(getPropertyValue(x), $"%{searchedText}%"));
            return query;
        }


        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> query, int itemsPerPage, int pageNum)
        {
            var list = query.Skip(pageNum <= 1 ? 0 : itemsPerPage * pageNum - 1).Take(itemsPerPage);

            var totalPages = (int)Math.Ceiling((double)(query.Count() / itemsPerPage));
            return new PagedList<T>()
            {
                Page = pageNum,
                Items = list.AsQueryable(),
                TotalPages = totalPages < 1 ? 1 : totalPages,
                HasNextPage = pageNum < totalPages,
                HasPreviousPage = pageNum > 1
            };
        }

        public static TResult Match<T, TResult>(this IEnumerable<T> enumerable,
            Func<IEnumerable<T>, TResult> methodWhenSome,
            Func<TResult> methodWhenNone) => enumerable.Any() ? methodWhenSome(enumerable) : methodWhenNone();

        public static TResult Match<T, TResult>(this IPagedList<T> pagedList,
            Func<IPagedList<T>, TResult> methodWhenSome,
            Func<TResult> methodWhenNone) => pagedList.Items.Any() ? methodWhenSome(pagedList) : methodWhenNone();
    }
}
