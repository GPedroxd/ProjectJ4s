using System;
using System.Collections.Generic;

namespace ProjectJ4s.Models
{
    public class Paging
    {
        public List<Person> People { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }

        public Paging()
        {
            People = new List<Person>();
        }
        public Paging(int pageSize, int currentPage)
        {
            PageSize = pageSize;
            CurrentPage = currentPage;
            People = new List<Person>();
        }
    }
}
