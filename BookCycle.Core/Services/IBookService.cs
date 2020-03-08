using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookCycle.Core.Models;

namespace BookCycle.Core.Services
{
    public interface IBookService : IService<Book>
    {
        Task<Book> GetWithCategoryByIdAsync(int bookId);
    }
}
