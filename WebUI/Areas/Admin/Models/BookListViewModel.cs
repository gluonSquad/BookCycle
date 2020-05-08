using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Concrete;

namespace WebUI.Areas.Admin.Models
{
    public class BookListViewModel
    {
        public int Id { get; set; }

        public string Isbn { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int BookPages { get; set; }

        public string BookImageUrl { get; set; }

        public int Rating { get; set; }

        public string BookPublisher { get; set; }

        public string DatePublished { get; set; }

        public int CategoryId { get; set; }

        public int AuthorId { get; set; }

        public DateTime CreatedOn { get; set; }
        public virtual Author Author { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<ReviewListViewModel> Reviews { get; set; }

        public virtual ICollection<QuotationViewModel> Quotations { get; set; }
     
    }

    public class ReviewListViewModel
    {
        public int Id { get; set; }

        public string HeadLine { get; set; }

        public string ReviewText { get; set; }

        public string BookStatus { get; set; }

        public int ReviewLike { get; set; }

        public int Rating { get; set; }

        public DateTime? CreatedOn { get; set; }
        public AppUser AppUser{ get; set; }
    }
    public class AppUserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class AuthorViewModel
    {
      
        public string Name { get; set; }

      
    }
    public class QuotationViewModel
    {


        public string QuotationText { get; set; }

        public int CurrentPage { get; set; }

        public int QuotesLike { get; set; }

        public DateTime? CreatedOn { get; set; }

        public AppUser AppUser{ get; set; }
    }
    public class CategoryViewModel
    {

        public string Name { get; set; }

    }
}
