using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfAuthorDal : EfEntityRepositoryBase<Author> , IAuthorDal
    {
        public EfAuthorDal(BookCycleContext bookCycleContext) : base(bookCycleContext)
        {

        }
    }
}
