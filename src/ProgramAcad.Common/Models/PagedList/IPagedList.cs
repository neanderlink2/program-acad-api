using System.Collections.Generic;

namespace ProgramAcad.Common.Models.PagedList
{
    public interface IPagedList<T>
    {
        int TotalPages { get; set; }
        int PageIndex { get; set; }
        IList<T> Items { get; set; }
        public int TotalItems { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
    }
}
