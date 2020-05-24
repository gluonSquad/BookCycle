using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CommentManager : ICommentService
    {
        private IReviewDal _reviewDal;

        public CommentManager(IReviewDal reviewDal)
        {
            _reviewDal = reviewDal;
        }
        public void Add(Review review)
        {
            _reviewDal.Add(review);
        }
    }
}
