using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookCycle.Core.Models;

namespace BookCycle.Core.Services
{
    public interface ICategoryService:IService<Category>
    {
        Task<Category> GetWithBooksByIdAsync(int categoryId);
    }
}
