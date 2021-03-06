﻿using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess;
using Core.Utilities;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IBookDal : IEntityRepository<Book>
    {
        //List<Book> MapToBookForList();
        List<Book> GetBookWithEagerLoading();
        
        List<Book> GetBookList(out int totalPage, string searchWord, int currentPage);
    }
}
