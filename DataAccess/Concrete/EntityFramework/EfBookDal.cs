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
                var books = context.Books.Include(b=> b.BookAppUsers).Include(b => b.Reviews).ThenInclude(br => br.AppUser).Include(b => b.Quotations).ThenInclude(bq=>bq.AppUser).Include(b => b.Author).Include(b => b.Category).OrderByDescending(I=>I.CreatedOn).ToList();
                return books;
            }
        }

        public List<Book> GetBookList(out int totalPage, string searchWord, int currentPage)
        {
            using var context = new BookCycleContext();


            var result = context.Books.Join(context.Categories, book => book.CategoryId, category => category.Id,
                (resultBook, resultCategory) => new
                {
                    book = resultBook,
                    category = resultCategory
                }).Join(context.Authors, twoTableResult => twoTableResult.book.AuthorId, author => author.Id,
                (resultTable, resultAuthor) => new
                {
                    book = resultTable.book,
                    category = resultTable.category,
                    author = resultAuthor
                }).Select(I => new Book()
            {
                Id = I.book.Id,
                Isbn = I.book.Isbn,
                CategoryId = I.book.CategoryId,
                AuthorId = I.book.AuthorId,
                Title = I.book.Title,
                BookPublisher = I.book.BookPublisher,
                BookPages = I.book.BookPages,
                BookImageUrl = I.book.BookImageUrl,
                DatePublished = I.book.DatePublished,
                Description = I.book.Description,
                Rating = I.book.Rating,
                Author = I.book.Author,
                Category = I.book.Category,
                CreatedOn = I.book.CreatedOn,

            });
            totalPage = (int)Math.Ceiling((double)result.Count() / 12);

            if (!string.IsNullOrWhiteSpace(searchWord))
            {
                result = result.Where(I =>
                    I.Title.ToLower().Contains(searchWord.ToLower()) ||
                    I.Author.Name.ToLower().Contains(searchWord.ToLower()));

                totalPage = (int)Math.Ceiling((double)result.Count() / 12);
            }

            result = result.Skip((currentPage - 1) * 12).Take(12);

            return result.ToList();
        }

     

       
    }
}
