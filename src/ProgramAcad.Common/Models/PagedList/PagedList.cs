using System.Linq;

namespace ProgramAcad.Common.Models.PagedList
{
    public class PagedList<T> : IPagedList<T>
    {
        public int TotalPages { get; set; }
        public int Page { get; set; }
        public IQueryable<T> Items { get; set; }
    }
}
