using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;

namespace DataAccess.Concrete.Abstract
{
    public interface IBookAppUserDal
    {
        public List<Book> GetByAppUserId(int appUserId);
        public int AddBookAppUser(int bookId, int appUserId);

    }
}
