using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookCycle.Core.Repositories;

namespace BookCycle.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {

        IBookRepository Books { get; }
        ICategoryRepository Categories { get; }
        IAuthorRepository Authors { get; }

        IUserRepository Users { get; }

        ICommentRepository Comments { get; }

        Task SaveChangesAsync();

        void SaveChanges();
    }
}
