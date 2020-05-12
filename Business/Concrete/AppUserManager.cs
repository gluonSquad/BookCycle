using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Business.Abstract;
using Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Abstract;

namespace Business.Concrete
{
    public class AppUserManager : IAppUserService
    {
        private IAppUserDal _userDal;

        public AppUserManager(IAppUserDal userDal)
        {
            _userDal = userDal;
        }

        public List<AppUser> GetNotAdmin()
        {
            return _userDal.GetNotAdmin();

        }

        public List<AppUser> GetNotAdmin(string searchWord, int currentPage)
        {
            return _userDal.GetNotAdmin(searchWord, currentPage);
        }
    }
}
