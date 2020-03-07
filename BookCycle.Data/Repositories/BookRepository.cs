using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookCycle.Core.Models;
using BookCycle.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookCycle.Data.Repositories
{
    public class BookRepository : Repository<Book> , IBookRepository
    {
        private BookCycleDbContext _bookCycleDbContext
        {
            get => _context as BookCycleDbContext;
        }
        public BookRepository(BookCycleDbContext context) : base(context)
        {
        }

        public async  Task<Book> GetWithCategoryByIdAsync(int bookId)
        {
            return await _bookCycleDbContext.Books.Include(x => x.Category).SingleOrDefaultAsync(x => x.Id == bookId);
        }
    }
}
