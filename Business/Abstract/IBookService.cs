using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities;

namespace Business.Abstract
{
    public interface IBookService
    {
        IDataResult<Book> GetById(int bookId);
        IDataResult<List<Book>> GetList();
        IDataResult<List<Book>> GetListByCategory(int categoryId);

        IResult Add(Book book);

        IResult Delete(Book book);

        IResult Update(Book book);
    }
}
