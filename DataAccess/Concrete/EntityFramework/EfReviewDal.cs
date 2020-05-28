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
    public class EfReviewDal : EfEntityRepositoryBase<Review, BookCycleContext>, IReviewDal
    {
        public List<Review> GetComments(out int totalPage, string searchWord, int currentPage)
        {
            using var context = new BookCycleContext();

            var result = context.Reviews.Join(context.Books, review => review.BookId, book => book.Id,
                (resultReview, resultBook) => new
                {
                    review = resultReview,
                    book = resultBook
                }).Join(context.Users, twoTableResult => twoTableResult.review.AppUser.Id, user => user.Id, (resultTable, resultUser) => new {
                    book = resultTable.book,
                    review = resultTable.review,
                    user = resultUser
                }).Join(context.Authors, threeTableResult => threeTableResult.review.Book.AuthorId, author => author.Id, (threeTable, resultAuthor) => new {
                    book = threeTable.book,
                    review = threeTable.review,
                    user = threeTable.user,
                    author = resultAuthor
                }).Select(I => new Review()
                {

                    Id = I.review.Id,
                    AppUser = new AppUser { FirstName = I.user.FirstName, LastName = I.user.LastName },
                    BookStatus = I.review.BookStatus,
                    CreatedOn = I.review.CreatedOn,
                    HeadLine = I.review.HeadLine,
                    Rating = I.review.Rating,
                    ReviewLike = I.review.ReviewLike,
                    ReviewText = I.review.ReviewText,
                    BookId = I.review.BookId,
                    AppUserId = I.review.AppUserId,
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

        public List<Review> GetComments()
        {
            using var context = new BookCycleContext();

            var result = context.Reviews.Join(context.Books, review => review.BookId, book => book.Id,
                (resultReview, resultBook) => new
                {
                    review = resultReview,
                    book = resultBook
                }).Join(context.Users, twoTableResult => twoTableResult.review.AppUser.Id, user => user.Id, (resultTable, resultUser) => new {
                    book = resultTable.book,
                    review = resultTable.review,
                    user = resultUser
                }).Join(context.Authors, threeTableResult => threeTableResult.review.Book.AuthorId, author => author.Id, (threeTable, resultAuthor) => new {
                    book = threeTable.book,
                    review = threeTable.review,
                    user = threeTable.user,
                    author = resultAuthor
                }).Select(I => new Review()
                {

                    Id = I.review.Id,
                    AppUser = new AppUser { FirstName = I.user.FirstName, LastName = I.user.LastName },
                    BookStatus = I.review.BookStatus,
                    CreatedOn = I.review.CreatedOn,
                    HeadLine = I.review.HeadLine,
                    Rating = I.review.Rating,
                    ReviewLike = I.review.ReviewLike,
                    ReviewText = I.review.ReviewText,
                    BookId = I.review.BookId,
                    AppUserId = I.review.AppUserId,
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
                });

            return result.ToList();
        }
    }
}
