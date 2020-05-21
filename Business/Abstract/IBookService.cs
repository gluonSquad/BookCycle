using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities;
using DataTransferObjects;

namespace Business.Abstract
{
    public interface IBookService
    {
        Book GetById(int bookId);
        List<Book> GetList();
        List<Book> GetListByCategory(int categoryId);

        void Add(Book book);

        void Delete(Book book);

        void Update(Book book);

        public bool CheckIsbnAndTitle(string isbn, string title);
        List<Book> GetBookList(out int totalPage, string searchWord, int currentPage);

    }
}
