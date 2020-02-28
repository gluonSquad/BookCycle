using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookCycle.Core.Repositories;
using BookCycle.Core.UnitOfWorks;
using BookCycle.Data.Repositories;

namespace BookCycle.Data.UnitOfWorks
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly BookCycleDbContext _context;

        private BookRepository _bookRepository;
        private CategoryRepository _categoryRepository;
        private AuthorRepository _authorRepository;
        private CommentRepository _commentRepository;
        private UserRepository _userRepository;

        public IBookRepository Books => _bookRepository = _bookRepository ?? new BookRepository(_context);

        public ICategoryRepository Categories =>
            _categoryRepository = _categoryRepository ?? new CategoryRepository(_context);

        public IAuthorRepository Authors => _authorRepository = _authorRepository ?? new AuthorRepository(_context);
        public IUserRepository Users => _userRepository = _userRepository ?? new UserRepository(_context);

        public ICommentRepository Comments =>
            _commentRepository = _commentRepository ?? new CommentRepository(_context);

        public UnitOfWork(BookCycleDbContext bookCycleDbContext)
        {
            _context = bookCycleDbContext;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
