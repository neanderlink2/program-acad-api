using System.Linq;

namespace ProgramAcad.Common.Models.PagedList
{
    public interface IPagedList<T>
    {
        int TotalPages { get; set; }
        int Page { get; set; }
        IQueryable<T> Items { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
    }
}
