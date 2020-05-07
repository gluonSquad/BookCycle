using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class BookCycleContext : IdentityDbContext<AppUser , AppRole ,int>
    {
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer(@"Server=.;Database=BookCycle;Trusted_Connection=true");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relation Key tanımlamalar BookAppUser'un PK'si
            modelBuilder.Entity<BookAppUser>()
                .HasKey(ba => new { ba.BookId, ba.AppUserId });

            // Relation Key tanımlamalar BookAppUser'un FK'si / BookAppUser -> Book
            modelBuilder.Entity<BookAppUser>()
                .HasOne(ba => ba.Book)
                .WithMany(b => b.BookAppUsers)
                .HasForeignKey(ba => ba.BookId);

            // Relation Key tanımlamalar BookAppUser'un FK'si / BookAppUser -> AppUser
            modelBuilder.Entity<BookAppUser>()
                .HasOne(ba => ba.AppUser)
                .WithMany(a => a.BookAppUsers)
                .HasForeignKey(ba => ba.AppUserId);

            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.Reviews)
                .WithOne(r => r.AppUser);
            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.Quotations)
                .WithOne(q => q.AppUser);

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

            base.OnModelCreating(modelBuilder);

        }


        public DbSet<Category> Categories { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Quotation> Quotations { get; set; }
        public DbSet<BookAppUser> BookAppUsers { get; set; }
    }
}
