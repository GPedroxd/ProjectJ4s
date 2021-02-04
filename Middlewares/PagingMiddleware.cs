using System;
using System.Collections.Generic;
using ProjectJ4s.DAO;
using ProjectJ4s.Models;

namespace ProjectJ4s.Middlewares
{
    public class PagingMiddleware
    {
        public PersonDAO PersonDAO { get; set; }
        public Paging Paging { get; set; }

        public PagingMiddleware(PersonDAO personDAO)
        {
            Paging = new Paging();
            PersonDAO = personDAO;
        }

        public Paging List(int pageSize, int currentPage)
        { 
            if (currentPage > GetTotalPage(pageSize))
            {
                currentPage = GetTotalPage(pageSize);
            }

            Paging.TotalPage = GetTotalPage(pageSize);
            Paging.CurrentPage = currentPage;
            Paging.PageSize = pageSize;
            int skip = pageSize * (currentPage - 1);
            Paging.People = PersonDAO.ListAll(pageSize, skip);

            return Paging;
            
        }
        public int GetTotalPage(int pageSize)
        {
            int totalPessoas = PersonDAO.GetTotal();
            double totalPage = (double)totalPessoas / (double)pageSize;
            return (int)Math.Ceiling(totalPage);
            
        }
    }
}
