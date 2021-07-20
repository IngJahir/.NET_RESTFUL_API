namespace SocialMedia.CORE.CustomEntities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class PagedList<T>: List<T>
    {
        public PagedList(List<T> items, int count, int pageSize, int pageNumber )
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        public static PagedList<T> Create(IEnumerable<T> source, int pageNumber, int pageSize) 
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1)*pageSize).Take(pageSize).ToList();
            return new PagedList<T> (items, count, pageNumber, pageSize);
        }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public bool HasPreviosPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public int? NextPageNumber => HasNextPage ? CurrentPage + 1: (int?)null;
        public int? PreviousPageNumber => HasPreviosPage ? CurrentPage - 1 : (int?)null;
    }
}
