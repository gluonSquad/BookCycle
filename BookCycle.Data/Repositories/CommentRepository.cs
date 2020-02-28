using System;
using System.Collections.Generic;
using System.Text;
using BookCycle.Core.Models;
using BookCycle.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookCycle.Data.Repositories
{
    public class CommentRepository : Repository<Comment> , ICommentRepository
    {
        public CommentRepository(DbContext context) : base(context)
        {
        }
    }
}
