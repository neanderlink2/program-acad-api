using System.Collections.Generic;

namespace ProgramAcad.Common.Models.PagedList
{
    public interface IPagedList<T>
    {
        int TotalPages { get; set; }
        int PageIndex { get; set; }
        IList<T> Items { get; set; }
        int TotalItems { get; set; }
        bool HasNextPage { get; set; }
        bool HasPreviousPage { get; set; }
    }
}
