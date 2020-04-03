using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using DataAccess.Abstract;
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

        public List<Book> GetList()
        {
            return _bookDal.GetList().ToList();
        }

        public List<Book> GetListByCategory(int categoryId)
        {
            return _bookDal.GetList(b => b.CategoryId == categoryId).ToList();
        }

        public void Update(Book book)
        {
           _bookDal.Update(book);
        }
    }
}
