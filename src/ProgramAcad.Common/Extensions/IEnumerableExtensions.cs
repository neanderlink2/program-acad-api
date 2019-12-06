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
            var skip = itemsPerPage * pageNum <= itemsPerPage ? 0 : itemsPerPage * pageNum;
            var list = query.Skip(skip).Take(itemsPerPage);

            if (pageNum <= 0)
            {
                throw new ArgumentOutOfRangeException("Page number must be greater than zero.");
            }

            return new PagedList<T>()
            {
                Page = pageNum,
                Items = list.AsQueryable(),
                TotalPages = (int)Math.Ceiling((double)(query.Count() / itemsPerPage))
            };
        }
    }
}
