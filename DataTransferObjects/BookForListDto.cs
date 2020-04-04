using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataTransferObjects
{
    public class BookForListDto
    {
        public int Id { get; set; }

        public string Isbn { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int BookPages { get; set; }

        public string BookImageUrl { get; set; }

        public int Rating { get; set; }

        public string BookPublisher { get; set; }

        public DateTime DatePublished { get; set; }

        public int CategoryId { get; set; }

        public int AuthorId { get; set; }

        public virtual AuthorDto Author { get; set; }

        public virtual CategoryDto Category { get; set; }

        public virtual ICollection<ReviewDto> Reviews { get; set; }

        public virtual ICollection<QuotationDto> Quotations { get; set; }
    }

    public class ReviewDto
    {
        public int Id { get; set; }

        public string HeadLine { get; set; }

        public string ReviewText { get; set; }

        public string BookStatus { get; set; }

        public int ReviewLike { get; set; }

        public int Rating { get; set; }

        public DateTime? CreatedOn { get; set; }
        public UserDto User { get; set; }
    }
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class AuthorDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
    public class QuotationDto
    {
        public int Id { get; set; }

        public string QuotationText { get; set; }

        public int CurrentPage { get; set; }

        public int QuotesLike { get; set; }

        public DateTime? CreatedOn { get; set; }
    }
    public class CategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
}

