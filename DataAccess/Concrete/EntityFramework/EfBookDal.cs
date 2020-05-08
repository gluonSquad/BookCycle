using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DataAccess.EntityFramework;
using Core.Utilities;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBookDal : EfEntityRepositoryBase<Book, BookCycleContext>, IBookDal
    {
      
        //public List<Book> MapToBookForList()
        //{
        //    using (var context = new BookCycleContext())
        //    {
        //        var books = context.Books.Include(b => b.Reviews).ThenInclude(br=>br.AppUser).Include(b => b.Quotations).Include(b=>b.Author).Include(b=>b.Category).ToList();
        //        return books;
        //    }
        //

        public List<Book> GetBookWithEagerLoading()
        {
            using (var context = new BookCycleContext())
            {
                var books = context.Books.Include(b => b.Reviews).ThenInclude(br => br.AppUser).Include(b => b.Quotations).Include(b => b.Author).Include(b => b.Category).ToList();
                return books;
            }
        }
    }
}
