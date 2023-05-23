using Microsoft.EntityFrameworkCore;

namespace quest_web_dotnet.Controllers
{
    //public class PaginatedList<T>: List<T>
    //{
    //    public int PageIndex { get; private set; }
    //    public int TotalPages { get; private set; }
    //    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
    //    {
    //        PageIndex = pageIndex;
    //        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

    //        // this.AddRange(items);
    //    }

    //    public bool HasPreviousPage => PageIndex > 1;

    //    public bool HasNextPage => PageIndex < TotalPages;
    //    public static async Task<PaginatedList<T>> Create(IQueryable<T> source, int pageIndex, int pageSize)
    //    {
    //        var count = source.Count();
    //        var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
    //        return new PaginatedList<T>(items, count, pageIndex, pageSize);
    //    }
    //}

    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public static PagedList<T> toPagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }

    public class PaginationParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
