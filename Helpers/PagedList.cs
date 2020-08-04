using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalentHunt.Helpers
{
    public class PagedList<T>: List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int ItemsCount { get; set; }
        public int PageSize { get; set; }

        public PagedList(List<T> items, int itemsCount, int pageNumber, int pageSize)
        {
            ItemsCount = itemsCount;
            CurrentPage = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling((double)ItemsCount / (double)PageSize);

            this.AddRange(items);
        }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            // calculate total count of source
            var itemsCount = await source.CountAsync();

            // calculate total items
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedList<T>(items, itemsCount, pageNumber, pageSize);
        }
    }
}
