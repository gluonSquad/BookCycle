using System;
using System.Collections.Generic;
using System.Text;
using BookCycle.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookCycle.Data.Seeds
{
    public class CategorySeed : IEntityTypeConfiguration<Category>
    {
        private readonly int[] _ids;
        public CategorySeed(int[] ids)
        {
            _ids = ids;
        }
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(new Category { Id = _ids[0], Name = "Roman" },
                new Category { Id = _ids[1], Name = "Deneme" },
                new Category { Id = _ids[2], Name = "Bilim-Kurgu" });
        }
    }
}
