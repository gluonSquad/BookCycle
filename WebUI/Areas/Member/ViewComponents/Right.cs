using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Areas.Member.Models;
using WebUI.Extensions;

namespace WebUI.Areas.Member.ViewComponents
{
    public class Right : ViewComponent
    {
        private ICommentService _commentService;
        private IQuotationService _quotationService;
        private IBookService _bookService;
        private IAppUserService _appUserService;

        public Right(ICommentService commentService, IQuotationService quotationService, IAppUserService appUserService, IBookService bookService)
        {
            _commentService = commentService;
            _quotationService = quotationService;
            _appUserService = appUserService;
            _bookService = bookService;
        }

        public IViewComponentResult Invoke()
        {


            RightModel model = new RightModel();

           
            List<Comment> listComment = new List<Comment>();

           
            List<User> listUser = new List<User>();

          
            List<Book> listBook = new List<Book>();

          
            List<Quotation> listQuotation = new List<Quotation>();


            var users = _appUserService.GetNotAdmin();
            users.ShuffleMethod();
            var fiveUsers = users.Take(3);
            foreach(var fiveUser in fiveUsers)
            {
                User user = new User();
                user.UserName = fiveUser.UserName;
                user.FirstName = fiveUser.FirstName;
                user.LastName = fiveUser.LastName;
                user.ProfileImageFile = fiveUser.ProfileImageFile;
                user.Description = fiveUser.Description;
                listUser.Add(user);
            }

            model.Users = listUser;


            var books = _bookService.GetList();
            books.ShuffleMethod();
            var fiveBooks = books.Take(4);
            
            foreach(var fiveBook in fiveBooks)
            {
                Book book = new Book();
                book.Title = fiveBook.Title;
                book.Id = fiveBook.Id;
                book.BookImageUrl = fiveBook.BookImageUrl;
                book.BookPublisher = fiveBook.BookPublisher;
                book.DatePublished = fiveBook.DatePublished;
                listBook.Add(book);
            }

            model.Books = listBook;

            var comments =  _commentService.GetComments();
            comments.ShuffleMethod();
            var fiveComments = comments.Take(3);
            foreach(var fivecomment in fiveComments)
            {
               
                Comment comment = new Comment();
                comment.Id = fivecomment.Id;
                comment.ReviewText = fivecomment.ReviewText;
                comment.Rating = fivecomment.Rating;
                comment.HeadLine = fivecomment.HeadLine;
                comment.CreatedOn = fivecomment.CreatedOn;
                comment.BookStatus = fivecomment.BookStatus;
                comment.BookId = fivecomment.BookId;
                comment.AppUserId = fivecomment.AppUserId;
                comment.Book = new Book
                {
                    Title = fivecomment.Book.Title,
                    BookImageUrl = fivecomment.Book.BookImageUrl
                };
                comment.User = new User
                {
                    UserName = fivecomment.AppUser.UserName,
                    FirstName = fivecomment.AppUser.FirstName,
                    LastName = fivecomment.AppUser.LastName
                };
                comment.User.LastName = fivecomment.AppUser.LastName;
                listComment.Add(comment);
            }

            model.Comments = listComment;

           

            var quotations = _quotationService.GetQuotations();
            quotations.ShuffleMethod();
            var fivequotations = quotations.Take(3);
            foreach(var fivequotation in fivequotations)
            {
                Quotation quotation = new Quotation();
                quotation.AppUserId = fivequotation.AppUserId;
                quotation.Id = fivequotation.Id;
                quotation.QuotationText = fivequotation.QuotationText;
                quotation.CurrentPage = fivequotation.CurrentPage;
                quotation.BookId = fivequotation.BookId;
                quotation.CreatedOn = fivequotation.CreatedOn;
                quotation.Book = new Book
                {
                    Title = fivequotation.Book.Title,
                    BookImageUrl = fivequotation.Book.BookImageUrl
                };
                quotation.User = new User
                {
                    UserName = fivequotation.AppUser.UserName,
                    FirstName = fivequotation.AppUser.FirstName,
                    LastName = fivequotation.AppUser.LastName
                };
                listQuotation.Add(quotation);
            }

            model.Quotations = listQuotation;

            return View(model);


        }
    }
}
