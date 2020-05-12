using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Concrete.Abstract
{
    public interface IAppUserDal 
    {
        List<AppUser> GetNotAdmin();
        List<AppUser> GetNotAdmin(string searchWord, int currentPage);
    }
}
