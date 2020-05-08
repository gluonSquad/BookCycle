using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Utilities;
using DataAccess.Abstract;
using DataTransferObjects;
using Entities.Concrete;

namespace Business.Concrete
{
    public class BookManager : IBookService
    {
        private IBookDal _bookDal;

        public BookManager(IBookDal bookDal)
        {
            _bookDal = bookDal;
        }
        public void Add(Book book)
        {
            _bookDal.Add(book);
           
        }

        public void Delete(Book book)
        {
            _bookDal.Delete(book);
            
        }

        public Book GetById(int bookId)
        {
            return _bookDal.Get(b => b.Id == bookId);
        }

        //public List<BookForListDto> GetList()
        //{
        //    throw new NotImplementedException();
        //}

        //public IDataResult<List<BookForListDto>> GetList()
        //{
        //    var result = _bookDal.MapToBookForList();
        //    var dto = MapToBookForListDto(result);
        //    return new SuccessDataResult<List<BookForListDto>>(dto);
        //}

        private List<Book> MapToBookForListDto(List<Book> books)
        {
            var returned = new List<Book>(books.Select(b => new Book
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
                Quotations = b.Quotations.Select(bq => new Quotation
                {
                    CreatedOn = bq.CreatedOn,
                    CurrentPage = bq.CurrentPage,
                    Id = bq.Id,
                    QuotationText = bq.QuotationText,
                    QuotesLike = bq.QuotesLike
                }).ToList(),
                Reviews = b.Reviews.Select(br => new Review
                {
                    Id = br.Id,
                    AppUser = new AppUser { FirstName = br.AppUser.FirstName, LastName = br.AppUser.LastName },
                    BookStatus = br.BookStatus,
                    CreatedOn = br.CreatedOn,
                    HeadLine = br.HeadLine,
                    Rating = br.Rating,
                    ReviewLike = br.ReviewLike,
                    ReviewText = br.ReviewText
                }).ToList()
            }));

            return books;
        }

        public List<Book> GetListByCategory(int categoryId)
        {
            return _bookDal.GetList(b => b.CategoryId == categoryId).ToList();
        }

        public void Update(Book book)
        {
            _bookDal.Update(book);
            
        }

        public List<Book> GetList()
        {
            var result = _bookDal.GetBookWithEagerLoading();
            var books = MapToBookForListDto(result);
            return books;

        }

        public bool CheckIsbnAndTitle(string isbn, string title)
        {
            var book = _bookDal.Get(b => b.Isbn == isbn && b.Title == title);
            if (book == null)
            {
                return true;
            }
            return false;
        }
    }
}
