using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GeneralStore.Api.Helpers
{
    public class PagedList<T>
    {
        public int CurrentPage { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages { get; }
        
        public bool HasNext => CurrentPage < TotalPages;

        public bool HasPrevious => CurrentPage > 1;

        public ReadOnlyCollection<T> Content => _content.AsReadOnly();

        public PagedList(IEnumerable<T> content, int currentPage, int pageSize, int totalCount)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = (int) (Math.Ceiling(totalCount / (double) pageSize));
            _content.AddRange(content); 
        }

        private List<T> _content = new List<T>();
    }
}
