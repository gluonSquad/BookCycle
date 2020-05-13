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
    }
}
