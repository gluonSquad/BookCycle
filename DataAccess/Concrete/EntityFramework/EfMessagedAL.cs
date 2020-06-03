using Core.DataAccess.EntityFramework;
using DataAccess.Concrete.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfMessageDal : EfEntityRepositoryBase<Message>, IMessageDal
    {
        public EfMessageDal(BookCycleContext bookCycleContext) : base(bookCycleContext)
        {

        }
    }
}
