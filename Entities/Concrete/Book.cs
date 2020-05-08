using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entities.Concrete
{
    public class Book : IEntity
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

        public virtual Author Author { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
        
        public virtual ICollection<Quotation> Quotations { get; set; }

        public virtual ICollection<BookAppUser> BookAppUsers { get; set; }


    }
}
