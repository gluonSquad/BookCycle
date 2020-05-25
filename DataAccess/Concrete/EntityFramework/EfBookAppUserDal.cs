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
            var result =  context.BookAppUsers.Where(I => I.AppUserId == appUserId).Include(I=>I.Book).Include(I=>I.Book.Quotations).Include(I=>I.Book.Reviews).Select(b=>new Book()
            {
                Author = new Author
                {
                    Name = b.Book.Author.Name,
                    Id = b.Book.Author.Id,
                },
                BookImageUrl = b.Book.BookImageUrl,
                BookPages = b.Book.BookPages,
                BookPublisher = b.Book.BookPublisher,
                Category = new Category { Id = b.Book.Category.Id, Name = b.Book.Category.Name },
                DatePublished = b.Book.DatePublished,
                Description = b.Book.Description,
                Id = b.Book.Id,
                Isbn = b.Book.Isbn,
                Rating = b.Book.Rating,
                Title = b.Book.Title,
                CreatedOn = b.Book.CreatedOn,
                Quotations = b.Book.Quotations.Select(bq => new Quotation
                {
                    CreatedOn = bq.CreatedOn,
                    CurrentPage = bq.CurrentPage,
                    Id = bq.Id,
                    QuotationText = bq.QuotationText,
                    QuotesLike = bq.QuotesLike
                }).ToList(),
                Reviews = b.Book.Reviews.Select(br => new Review
                {
                    Id = br.Id,
                    AppUser = new AppUser { FirstName = br.AppUser.FirstName, LastName = br.AppUser.LastName },
                    BookStatus = br.BookStatus,
                    CreatedOn = br.CreatedOn,
                    HeadLine = br.HeadLine,
                    Rating = br.Rating,
                    ReviewLike = br.ReviewLike,
                    ReviewText = br.ReviewText,
                    BookId = br.BookId,
                    AppUserId = br.AppUserId,
                    Book = new Book
                    {
                        Author = new Author
                        {
                            Name = br.Book.Author.Name,
                            Id = br.Book.Author.Id,
                        },
                        BookImageUrl = br.Book.BookImageUrl,
                        BookPages = br.Book.BookPages,
                        BookPublisher = br.Book.BookPublisher,
                        Category = new Category { Id = br.Book.Category.Id, Name = br.Book.Category.Name },
                        DatePublished = br.Book.DatePublished,
                        Description = br.Book.Description,
                        Id = br.Book.Id,
                        Isbn = br.Book.Isbn,
                        Rating = br.Book.Rating,
                        Title = br.Book.Title,
                        CreatedOn = br.Book.CreatedOn,
                    }
                }).ToList()
            });

            return result.ToList();

        }


        public List<Book> GetAll()
        {
            using var context = new BookCycleContext();
            var result = context.BookAppUsers.Include(I => I.Book).Include(I => I.Book.Quotations).Include(I => I.Book.Reviews).Select(b => new Book()
            {
                Author = new Author
                {
                    Name = b.Book.Author.Name,
                    Id = b.Book.Author.Id,
                },
                BookImageUrl = b.Book.BookImageUrl,
                BookPages = b.Book.BookPages,
                BookPublisher = b.Book.BookPublisher,
                Category = new Category { Id = b.Book.Category.Id, Name = b.Book.Category.Name },
                DatePublished = b.Book.DatePublished,
                Description = b.Book.Description,
                Id = b.Book.Id,
                Isbn = b.Book.Isbn,
                Rating = b.Book.Rating,
                Title = b.Book.Title,
                CreatedOn = b.Book.CreatedOn,
                Quotations = b.Book.Quotations.Select(bq => new Quotation
                {
                    CreatedOn = bq.CreatedOn,
                    CurrentPage = bq.CurrentPage,
                    Id = bq.Id,
                    QuotationText = bq.QuotationText,
                    QuotesLike = bq.QuotesLike
                }).OrderByDescending(b=>b.CreatedOn).ToList(),
                Reviews = b.Book.Reviews.Select(br => new Review
                {
                    Id = br.Id,
                    AppUser = new AppUser { FirstName = br.AppUser.FirstName, LastName = br.AppUser.LastName },
                    BookStatus = br.BookStatus,
                    CreatedOn = br.CreatedOn,
                    HeadLine = br.HeadLine,
                    Rating = br.Rating,
                    ReviewLike = br.ReviewLike,
                    ReviewText = br.ReviewText,
                    BookId = br.BookId,
                    AppUserId = br.AppUserId,
                    Book = new Book
                    {
                        Author = new Author
                        {
                            Name = br.Book.Author.Name,
                            Id = br.Book.Author.Id,
                        },
                        BookImageUrl = br.Book.BookImageUrl,
                        BookPages = br.Book.BookPages,
                        BookPublisher = br.Book.BookPublisher,
                        Category = new Category { Id = br.Book.Category.Id, Name = br.Book.Category.Name },
                        DatePublished = br.Book.DatePublished,
                        Description = br.Book.Description,
                        Id = br.Book.Id,
                        Isbn = br.Book.Isbn,
                        Rating = br.Book.Rating,
                        Title = br.Book.Title,
                        CreatedOn = br.Book.CreatedOn,
                    }
                }).OrderByDescending(b=>b.CreatedOn).ToList()
            });

            return result.ToList();

        }


        public List<Book> GetAll(out int totalPage, string searchWord, int currentPage)
        {
            using var context = new BookCycleContext();
            var result = context.BookAppUsers.Include(I => I.Book).Include(I => I.Book.Quotations).Include(I => I.Book.Reviews).Select(b => new Book()
            {
                Author = new Author
                {
                    Name = b.Book.Author.Name,
                    Id = b.Book.Author.Id,
                },
                BookImageUrl = b.Book.BookImageUrl,
                BookPages = b.Book.BookPages,
                BookPublisher = b.Book.BookPublisher,
                Category = new Category { Id = b.Book.Category.Id, Name = b.Book.Category.Name },
                DatePublished = b.Book.DatePublished,
                Description = b.Book.Description,
                Id = b.Book.Id,
                Isbn = b.Book.Isbn,
                Rating = b.Book.Rating,
                Title = b.Book.Title,
                CreatedOn = b.Book.CreatedOn,
                Quotations = b.Book.Quotations.Select(bq => new Quotation
                {
                    CreatedOn = bq.CreatedOn,
                    CurrentPage = bq.CurrentPage,
                    Id = bq.Id,
                    QuotationText = bq.QuotationText,
                    QuotesLike = bq.QuotesLike
                }).OrderByDescending(b => b.CreatedOn).ToList(),
                Reviews = b.Book.Reviews.Select(br => new Review
                {
                    Id = br.Id,
                    AppUser = new AppUser { FirstName = br.AppUser.FirstName, LastName = br.AppUser.LastName },
                    BookStatus = br.BookStatus,
                    CreatedOn = br.CreatedOn,
                    HeadLine = br.HeadLine,
                    Rating = br.Rating,
                    ReviewLike = br.ReviewLike,
                    ReviewText = br.ReviewText,
                    BookId = br.BookId,
                    AppUserId = br.AppUserId,
                    Book = new Book
                    {
                        Author = new Author
                        {
                            Name = br.Book.Author.Name,
                            Id = br.Book.Author.Id,
                        },
                        BookImageUrl = br.Book.BookImageUrl,
                        BookPages = br.Book.BookPages,
                        BookPublisher = br.Book.BookPublisher,
                        Category = new Category { Id = br.Book.Category.Id, Name = br.Book.Category.Name },
                        DatePublished = br.Book.DatePublished,
                        Description = br.Book.Description,
                        Id = br.Book.Id,
                        Isbn = br.Book.Isbn,
                        Rating = br.Book.Rating,
                        Title = br.Book.Title,
                        CreatedOn = br.Book.CreatedOn,
                    }
                }).OrderByDescending(b => b.CreatedOn).ToList()
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
