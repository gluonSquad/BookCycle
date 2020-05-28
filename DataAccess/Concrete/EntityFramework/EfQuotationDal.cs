using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfQuotationDal : EfEntityRepositoryBase<Quotation, BookCycleContext>, IQuotationDal
    {
        public List<Quotation> GetQuotations(out int totalPage, string searchWord, int currentPage)
        {
            using var context = new BookCycleContext();

            var result = context.Quotations.Join(context.Books, quotation=> quotation.BookId, book=> book.Id,
                (resultQuotation, resultBook) => new
                {
                    quotation = resultQuotation,
                    book = resultBook
                }).Join(context.Users , twoTableResult => twoTableResult.quotation.AppUser.Id , user=>user.Id,(resultTable , resultUser) => new { 
                    book = resultTable.book,
                    quotation = resultTable.quotation,
                    user = resultUser
                }).Join(context.Authors, threeTableResult => threeTableResult.quotation.Book.AuthorId, author => author.Id , (threeTable , resultAuthor)=>new { 
                    book = threeTable.book,
                    quotation = threeTable.quotation,
                    user = threeTable.user,
                    author = resultAuthor
                }).Select(I => new Quotation()
                {
                    
                    Id = I.quotation.Id,
                    CurrentPage = I.quotation.CurrentPage,
                    CreatedOn = I.quotation.CreatedOn,
                    BookId = I.quotation.BookId,
                    AppUserId = I.quotation.AppUserId,
                    Book = new Book
                    {
                        Author = new Author
                        {
                            Name = I.author.Name,
                            Id = I.author.Id,
                        },
                        BookImageUrl = I.book.BookImageUrl,
                        BookPages = I.book.BookPages,
                        BookPublisher = I.book.BookPublisher,
                        DatePublished = I.book.DatePublished,
                        Description = I.book.Description,
                        Id = I.book.Id,
                        Isbn = I.book.Isbn,
                        Rating = I.book.Rating,
                        Title = I.book.Title,
                        CreatedOn = I.book.CreatedOn,
                    },
                    AppUser = I.quotation.AppUser,
                    QuotationText = I.quotation.QuotationText,
                    QuotesLike = I.quotation.QuotesLike,
                    
                });



            totalPage = (int)Math.Ceiling((double)result.Count() / 12);

            if (!string.IsNullOrWhiteSpace(searchWord))
            {
                result = result.Where(I =>
                     I.Book.Title.ToLower().Contains(searchWord.ToLower()) ||
                     I.Book.Author.Name.ToLower().Contains(searchWord.ToLower()));

                totalPage = (int)Math.Ceiling((double)result.Count() / 12);
            }

            result = result.Skip((currentPage - 1) * 12).Take(12);

            return result.ToList();
        }



        public List<Quotation> GetQuotations()
        {
            using var context = new BookCycleContext();

            var result = context.Quotations.Join(context.Books, quotation => quotation.BookId, book => book.Id,
                (resultQuotation, resultBook) => new
                {
                    quotation = resultQuotation,
                    book = resultBook
                }).Join(context.Users, twoTableResult => twoTableResult.quotation.AppUser.Id, user => user.Id, (resultTable, resultUser) => new
                {
                    book = resultTable.book,
                    quotation = resultTable.quotation,
                    user = resultUser
                }).Join(context.Authors, threeTableResult => threeTableResult.quotation.Book.AuthorId, author => author.Id, (threeTable, resultAuthor) => new
                {
                    book = threeTable.book,
                    quotation = threeTable.quotation,
                    user = threeTable.user,
                    author = resultAuthor
                }).Select(I => new Quotation()
                {

                    Id = I.quotation.Id,
                    CurrentPage = I.quotation.CurrentPage,
                    CreatedOn = I.quotation.CreatedOn,
                    BookId = I.quotation.BookId,
                    AppUserId = I.quotation.AppUserId,
                    Book = new Book
                    {
                        Author = new Author
                        {
                            Name = I.author.Name,
                            Id = I.author.Id,
                        },
                        BookImageUrl = I.book.BookImageUrl,
                        BookPages = I.book.BookPages,
                        BookPublisher = I.book.BookPublisher,
                        DatePublished = I.book.DatePublished,
                        Description = I.book.Description,
                        Id = I.book.Id,
                        Isbn = I.book.Isbn,
                        Rating = I.book.Rating,
                        Title = I.book.Title,
                        CreatedOn = I.book.CreatedOn,
                    },
                    AppUser = I.quotation.AppUser,
                    QuotationText = I.quotation.QuotationText,
                    QuotesLike = I.quotation.QuotesLike,

                });

            return result.ToList();
        }
    }
}
