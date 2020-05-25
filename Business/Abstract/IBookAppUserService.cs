using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IBookAppUserService
    {
        List<Book> GetByAppUserId(int appUserId);
        int AddBookAppUser(int bookId, int appUserId);

        List<Book> GetAll();
        List<Book> GetAll(out int totalPage, string searchWord, int currentPage);
    }
}
