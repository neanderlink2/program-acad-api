using System.Collections.Generic;

namespace ProgramAcad.Common.Models.PagedList
{
    public class PagedList<T> : IPagedList<T>
    {
        public int TotalPages { get; set; }
        public int PageIndex { get; set; }
        public IList<T> Items { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public int TotalItems { get; set; }
    }
}
