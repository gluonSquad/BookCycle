using System;
using System.Collections.Generic;
using System.Text;
using BookCycle.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookCycle.Data.Configurations
{
    public class UserConfiguration:IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Name).HasMaxLength(30);
            builder.Property(x => x.LastName).HasMaxLength(30);
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Mail).IsRequired().HasMaxLength(70);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(30);
            builder.Property(x => x.ProfileImageFile).HasMaxLength(30);
            builder.Property(x => x.Telephone).HasMaxLength(11);

            builder.ToTable("Users");

        }
    }
}
