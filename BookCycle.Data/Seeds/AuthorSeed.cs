using System;
using System.Collections.Generic;
using System.Text;
using BookCycle.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookCycle.Data.Seeds
{
    public class AuthorSeed : IEntityTypeConfiguration<Author>
    {
        private readonly int[] _ids;
        
        public AuthorSeed(int[] ids)
        {
            _ids = ids;
        }
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasData(new Author {Id = _ids[0], Name = "Jose Mauro De Vasconcelos"},
                new Author {Id = _ids[1], Name = "Ali Ural"},
                new Author {Id = _ids[2], Name = "Douglas Adams"},
                new Author {Id = _ids[3], Name = "Zülfü Livaneli" });
        }
    }
}
