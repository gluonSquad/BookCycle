using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookCycle.Core.Models;

namespace BookCycle.Core.Repositories
{
    public interface IBookRepository:IRepository<Book>
    {
        Task<Book> GetWithCategoryByIdAsync(int bookId);
    }
}
