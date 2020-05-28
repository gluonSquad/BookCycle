using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Areas.Member.Models
{
    public class RightModel
    {
        public List<Comment> Comments { get; set; }
        public List<Quotation> Quotations { get; set; }
        public List<User> Users { get; set; }

        public List<Book> Books{ get; set; }
    }


    public class Comment
    {
        public int Id { get; set; }

        public string HeadLine { get; set; }

        public string ReviewText { get; set; }

        public string BookStatus { get; set; }

        public int ReviewLike { get; set; }

        public int Rating { get; set; }

        public DateTime? CreatedOn { get; set; } = DateTime.Now;

        public Book  Book { get; set; }
        public User User { get; set; }
        public int? BookId { get; set; }

        public int? AppUserId { get; set; }

    }


    public class Quotation
    {
        public int Id { get; set; }

        public string QuotationText { get; set; }

        public int CurrentPage { get; set; }

        public int QuotesLike { get; set; }

        public DateTime? CreatedOn { get; set; } = DateTime.Now;

        public Book Book { get; set; }
        public User User { get; set; }
        public int? BookId { get; set; }

        public int? AppUserId { get; set; }
      
    }

    public class User
    {
        public string ProfileImageFile { get; set; } = "default.png";

        public string Description { get; set; }
        public string UserName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Age { get; set; }


     
    }

    public class Book
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

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int CategoryId { get; set; }

        public int AuthorId { get; set; }

    
    }



}
