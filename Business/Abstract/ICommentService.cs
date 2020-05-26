using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICommentService
    {
        public void Add(Review review);
        public List<Review> GetComments(out int totalPage, string searchWord, int currentPage);
    }
}
