using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Concrete.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBookAppUserDal : IBookAppUserDal
    {
        public List<Book> GetByAppUserId(int appUserId)
        {
            using var context = new BookCycleContext();
            var result =  context.BookAppUsers.Where(I => I.AppUserId == appUserId).Select(z=>new Book()
            {
                Id = z.BookId,
                Title = z.Book.Title
            });

            return result.ToList();
        }
    }
}
