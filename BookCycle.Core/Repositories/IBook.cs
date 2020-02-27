using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookCycle.Core.Models;

namespace BookCycle.Core.Repositories
{
    public interface IBook:IRepository<Book>
    {
        Task<Book> GetWithCategoryByIdAsync(int bookId);
    }
}
