using System;
using System.Collections.Generic;
using System.Text;
using BookCycle.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookCycle.Data
{
    public class BookCycleDbContext : DbContext
    {
        public BookCycleDbContext(DbContextOptions<BookCycleDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<BorrowedBook> BorrowedBooks { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Liked> Likeds { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
