using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BookCycle.Core.Models
{
    public class Book:BaseModel
    {
        public Book()
        {
            Comments = new Collection<Comment>();
            Likes = new Collection<Liked>();
        }
        public string Name { get; set; }

        public string Description { get; set;  }

        public int BookPages { get; set; }

        public string BookPublisher { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Isbn { get; set; }

        public string BookImageFile { get; set; }

        public int AuthorId { get; set; }

        public int LikeCount { get; set; }

        public int CategoryId { get; set; }

        public Author Author { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Liked> Likes { get; set; }






    }
}
