using System;
using System.Collections.Generic;
using System.Text;
using BookCycle.Core.Models;
using BookCycle.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookCycle.Data.Repositories
{
    public class AuthorRepository:Repository<Author> , IAuthorRepository
    {
        public AuthorRepository(DbContext context) : base(context)
        {
        }
    }
}
