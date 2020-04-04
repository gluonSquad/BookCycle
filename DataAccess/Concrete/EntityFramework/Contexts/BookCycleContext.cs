using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class BookCycleContext : DbContext
    {
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer(@"Server=.;Database=BookCycle;Trusted_Connection=true");
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Quotation> Quotations { get; set; }


        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Reviews)
                .WithOne(r => r.User);
            modelBuilder.Entity<User>()
                .HasMany(u => u.Quotations)
                .WithOne(q => q.User);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Quotations)
                .WithOne(q => q.Book);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Reviews)
                .WithOne(r => r.Book);

            modelBuilder.Entity<Book>()
                .HasOne<Category>(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId);

            modelBuilder.Entity<Book>()
                .HasOne<Author>(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);

        }
    }
}
