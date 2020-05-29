using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using DataAccess.Concrete.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class BookAppUserManager : IBookAppUserService
    {
        private IBookAppUserDal _bookAppUserDal;

        public BookAppUserManager(IBookAppUserDal bookAppUserDal)
        {
            _bookAppUserDal = bookAppUserDal;
        }

        public List<Book> GetByAppUserId(int appUserId)
        {
            return _bookAppUserDal.GetByAppUserId(appUserId);
        }

        public int AddBookAppUser(int bookId, int appUserId)
        {
            return _bookAppUserDal.AddBookAppUser(bookId , appUserId);
        }

        public List<Book> GetAll()
        {
            return _bookAppUserDal.GetAll();
        }

        public List<Book> GetAll(out int totalPage, string searchWord, int currentPage)
        {
            return _bookAppUserDal.GetAll(out totalPage, searchWord, currentPage);
        }

        public Book GetBook(int bookId)
        {
            return _bookAppUserDal.GetBook(bookId);
        }
    }
}
