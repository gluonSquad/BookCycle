using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IAppUserService
    {
        List<AppUser> GetNotAdmin();
        List<AppUser> GetNotAdmin(string searchWord,int currentPage );
    }
}
