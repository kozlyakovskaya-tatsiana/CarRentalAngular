using System;
using System.Collections.Generic;

namespace CarRental.Service.Filter
{
    public class PagedCollection<T> where T : class
    {
        public PagedCollection(IEnumerable<T> itemsPerPage, int pageNumber, int totalPages)
        {
            ItemsPerPage = itemsPerPage;

            PageNumber = pageNumber;

            TotalPages = totalPages;
        }
        public IEnumerable<T> ItemsPerPage { get; }

        public int PageNumber { get; }

        public bool HasPreviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < TotalPages;

        public int TotalPages { get; }
    }
}