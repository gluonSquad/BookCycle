using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookCycle.Core.Models;
using BookCycle.Core.Repositories;
using BookCycle.Core.Services;
using BookCycle.Core.UnitOfWorks;

namespace BookCycle.Service.Services
{
    public class BookService:Service<Book> , IBookService
    {
        public BookService(IUnitOfWork unitOfWork, IRepository<Book> repository) : base(unitOfWork, repository)
        {
        }

        public  async Task<Book> GetWithCategoryByIdAsync(int bookId)
        {
            return await _unitOfWork.Books.GetWithCategoryByIdAsync(bookId);
        }
    }
}
