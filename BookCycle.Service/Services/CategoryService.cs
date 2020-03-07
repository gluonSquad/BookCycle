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
    public class CategoryService : Service<Category> , ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork, IRepository<Category> repository) : base(unitOfWork, repository)
        {
        }

        public async  Task<Category> GetWithBooksByIdAsync(int categoryId)
        {
            return await _unitOfWork.Categories.GetWithBooksByIdAsync(categoryId);
        }
    }
}
