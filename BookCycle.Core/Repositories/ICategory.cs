using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookCycle.Core.Models;

namespace BookCycle.Core.Repositories
{
    public interface ICategory:IRepository<Category>
    {
        Task<Category> GetWithBooksByIdAsync(int categoryId);
    }
}
