using System;
using System.Collections.Generic;
using System.Text;
using BookCycle.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookCycle.Data.Configurations
{
    public class BookConfiguration:IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(300);
            builder.Property(x => x.BookPages).IsRequired();
            builder.Property(x => x.Isbn).IsRequired().HasMaxLength(13);
            builder.Property(x => x.BookImageFile).HasMaxLength(100);
            builder.Property(x => x.BookPublisher).HasMaxLength(50);

            builder.ToTable("Books");
        }
    }
}
