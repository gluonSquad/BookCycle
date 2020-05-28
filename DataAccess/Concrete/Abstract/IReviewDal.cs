using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IReviewDal : IEntityRepository<Review>
    {
        public List<Review> GetComments(out int totalPage, string searchWord, int currentPage);
        public List<Review> GetComments();

    }
}
