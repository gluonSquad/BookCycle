using System;
using System.Collections.Generic;
using System.Text;
using BookCycle.Core.Models;
using BookCycle.Data.Configurations;
using BookCycle.Data.Seeds;
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
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new LikedConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new BorrowedBookConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());

            modelBuilder.ApplyConfiguration(new BookSeed(new int[] {1, 2, 3, 4}));
            modelBuilder.ApplyConfiguration(new CategorySeed(new int[] {1, 2, 3}));
            modelBuilder.ApplyConfiguration(new AuthorSeed(new int[] {1, 2, 3, 4}));
        }
    }
}
