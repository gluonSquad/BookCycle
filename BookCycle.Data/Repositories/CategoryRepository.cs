using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookCycle.Core.Models;
using BookCycle.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookCycle.Data.Repositories
{
    public class CategoryRepository:Repository<Category> , ICategoryRepository
    {
        private BookCycleDbContext _bookCycleDbContext
        {
            get => _context as BookCycleDbContext;
        }
        public CategoryRepository(DbContext context) : base(context)
        {
        }

        public async Task<Category> GetWithBooksByIdAsync(int categoryId)
        {
            return await _bookCycleDbContext.Categories.Include(x => x.Books)
                .SingleOrDefaultAsync(x => x.Id == categoryId);
        }
    }
}
