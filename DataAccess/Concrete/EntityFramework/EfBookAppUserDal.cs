using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Concrete.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBookAppUserDal : IBookAppUserDal
    {
        public int AddBookAppUser(int  bookId, int appUserId)
        {
            using var context = new BookCycleContext();
            var book = context.Books.First(i => i.Id == bookId);
            var appUser = context.Users.First(i => i.Id == appUserId);
            var bookList = GetByAppUserId(appUserId);
            foreach (var item in bookList)
            {
                if (item.Id == bookId)
                {
                    return 0;
                }
            }
            var bookAppUser = new BookAppUser
            {
                BookId = book.Id,
                AppUserId = appUser.Id
            };

            context.Entry(bookAppUser).State = EntityState.Added;
            return context.SaveChanges();
        }

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
