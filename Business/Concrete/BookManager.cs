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
        public IResult Add(Book book)
        {
            _bookDal.Add(book);
            return new SuccessResult(Messages.BookAdded);
        }

        public IResult Delete(Book book)
        {
            _bookDal.Delete(book);
            return new SuccessResult(Messages.BookDeleted);
        }

        public IDataResult<Book> GetById(int bookId)
        {
            return new SuccessDataResult<Book>(_bookDal.Get(b => b.Id == bookId));
        }

        public IDataResult<List<BookForListDto>> GetList()
        {
            var result = _bookDal.MapToBookForList();
            var dto = MapToBookForListDto(result);
            return new SuccessDataResult<List<BookForListDto>>(dto);
        }

        private List<BookForListDto> MapToBookForListDto(List<Book> books)
        {
            var returnedDto = new List<BookForListDto>(books.Select(b => new BookForListDto
            {
                Author = new AuthorDto
                {
                    FirstName = b.Author.FirstName,
                    Id = b.Author.Id,
                    LastName = b.Author.LastName
                },
                BookImageUrl = b.BookImageUrl,
                BookPages = b.BookPages,
                BookPublisher = b.BookPublisher,
                Category = new CategoryDto { Id = b.Category.Id, Name = b.Category.Name },
                DatePublished = b.DatePublished,
                Description = b.Description,
                Id = b.Id,
                Isbn = b.Isbn,
                Rating = b.Rating,
                Title = b.Title,
                Quotations = b.Quotations.Select(bq => new QuotationDto
                {
                    CreatedOn = bq.CreatedOn,
                    CurrentPage = bq.CurrentPage,
                    Id = bq.Id,
                    QuotationText = bq.QuotationText,
                    QuotesLike = bq.QuotesLike
                }).ToList(),
                Reviews = b.Reviews.Select(br => new ReviewDto
                {
                    Id = br.Id,
                    User = new UserDto { FirstName = br.User.FirstName, LastName = br.User.LastName },
                    BookStatus = br.BookStatus,
                    CreatedOn = br.CreatedOn,
                    HeadLine = br.HeadLine,
                    Rating = br.Rating,
                    ReviewLike = br.ReviewLike,
                    ReviewText = br.ReviewText
                }).ToList()
            }));
          
            return returnedDto;
        }

        public IDataResult<List<Book>> GetListByCategory(int categoryId)
        {
            return new SuccessDataResult<List<Book>>(_bookDal.GetList(b => b.CategoryId == categoryId).ToList());
        }

        public IResult Update(Book book)
        {
            _bookDal.Update(book);
            return new SuccessResult(Messages.BookUpdated);
        }

    }
}
