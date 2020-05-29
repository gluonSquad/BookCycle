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
                    QuotesLike = bq.QuotesLike,
                    AppUser = new AppUser { FirstName = bq.AppUser.FirstName, LastName = bq.AppUser.LastName },
                    BookId = bq.BookId,
                    AppUserId = bq.AppUserId,
                    Book = new Book
                    {
                        Author = new Author
                        {
                            Name = bq.Book.Author.Name,
                            Id = bq.Book.Author.Id,
                        },
                        BookImageUrl = bq.Book.BookImageUrl,
                        BookPages = bq.Book.BookPages,
                        BookPublisher = bq.Book.BookPublisher,
                        Category = new Category { Id = bq.Book.Category.Id, Name = bq.Book.Category.Name },
                        DatePublished = bq.Book.DatePublished,
                        Description = bq.Book.Description,
                        Id = bq.Book.Id,
                        Isbn = bq.Book.Isbn,
                        Rating = bq.Book.Rating,
                        Title = bq.Book.Title,
                        CreatedOn = bq.Book.CreatedOn,
                    }
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

            return result.ToList();

        }


        public Book GetBook(int bookId)
        {
            using var context = new BookCycleContext();
            var result = context.Books.Where(I => I.Id == bookId).Include(I => I.Reviews).Include(I => I.Quotations).Include(I => I.Category).Include(I=>I.BookAppUsers).Include(I=>I.Author).Select(b => new Book()
            {
                Author = new Author
                {
                    Name = b.Author.Name,
                    Id = b.Author.Id,
                },
                BookImageUrl = b.BookImageUrl,
                BookPages = b.BookPages,
                BookPublisher = b.BookPublisher,
                Category = new Category { Id = b.Category.Id, Name = b.Category.Name },
                DatePublished = b.DatePublished,
                Description = b.Description,
                Id = b.Id,
                Isbn = b.Isbn,
                Rating = b.Rating,
                Title = b.Title,
                CreatedOn = b.CreatedOn,
                Quotations = b.Quotations.Select(bq => new Quotation
                {
                    CreatedOn = bq.CreatedOn,
                    CurrentPage = bq.CurrentPage,
                    Id = bq.Id,
                    QuotationText = bq.QuotationText,
                    QuotesLike = bq.QuotesLike,
                    AppUser = new AppUser { FirstName = bq.AppUser.FirstName, LastName = bq.AppUser.LastName , UserName = bq.AppUser.UserName},
                    BookId = bq.BookId,
                    AppUserId = bq.AppUserId,
                    Book = new Book
                    {
                        Author = new Author
                        {
                            Name = bq.Book.Author.Name,
                            Id = bq.Book.Author.Id,
                        },
                        BookImageUrl = bq.Book.BookImageUrl,
                        BookPages = bq.Book.BookPages,
                        BookPublisher = bq.Book.BookPublisher,
                        Category = new Category { Id = bq.Book.Category.Id, Name = bq.Book.Category.Name },
                        DatePublished = bq.Book.DatePublished,
                        Description = bq.Book.Description,
                        Id = bq.Book.Id,
                        Isbn = bq.Book.Isbn,
                        Rating = bq.Book.Rating,
                        Title = bq.Book.Title,
                        CreatedOn = bq.Book.CreatedOn,
                    }
                }).OrderByDescending(b => b.CreatedOn).ToList(),

                Reviews = b.Reviews.Select(br => new Review
                {
                    Id = br.Id,
                    AppUser = new AppUser { FirstName = br.AppUser.FirstName, LastName = br.AppUser.LastName , UserName = br.AppUser.UserName },
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
            
            return result.First();

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
                    QuotesLike = bq.QuotesLike,
                    AppUser = new AppUser { FirstName = bq.AppUser.FirstName, LastName = bq.AppUser.LastName },
                    BookId = bq.BookId,
                    AppUserId = bq.AppUserId,
                    Book = new Book
                    {
                        Author = new Author
                        {
                            Name = bq.Book.Author.Name,
                            Id = bq.Book.Author.Id,
                        },
                        BookImageUrl = bq.Book.BookImageUrl,
                        BookPages = bq.Book.BookPages,
                        BookPublisher = bq.Book.BookPublisher,
                        Category = new Category { Id = bq.Book.Category.Id, Name = bq.Book.Category.Name },
                        DatePublished = bq.Book.DatePublished,
                        Description = bq.Book.Description,
                        Id = bq.Book.Id,
                        Isbn = bq.Book.Isbn,
                        Rating = bq.Book.Rating,
                        Title = bq.Book.Title,
                        CreatedOn = bq.Book.CreatedOn,
                    }
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
                    QuotesLike = bq.QuotesLike,
                    AppUser = new AppUser { FirstName = bq.AppUser.FirstName, LastName = bq.AppUser.LastName },
                    BookId = bq.BookId,
                    AppUserId = bq.AppUserId,
                    Book = new Book
                    {
                        Author = new Author
                        {
                            Name = bq.Book.Author.Name,
                            Id = bq.Book.Author.Id,
                        },
                        BookImageUrl = bq.Book.BookImageUrl,
                        BookPages = bq.Book.BookPages,
                        BookPublisher = bq.Book.BookPublisher,
                        Category = new Category { Id = bq.Book.Category.Id, Name = bq.Book.Category.Name },
                        DatePublished = bq.Book.DatePublished,
                        Description = bq.Book.Description,
                        Id = bq.Book.Id,
                        Isbn = bq.Book.Isbn,
                        Rating = bq.Book.Rating,
                        Title = bq.Book.Title,
                        CreatedOn = bq.Book.CreatedOn,
                    }
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
