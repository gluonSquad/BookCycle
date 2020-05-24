using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entities.Concrete
{
    public class Review : IEntity
    {
        public int Id { get; set; }

        public string HeadLine { get; set; }

        public string ReviewText { get; set; }

        public string BookStatus { get; set; }

        public int ReviewLike { get; set; }

        public  int Rating { get; set; }

        public DateTime? CreatedOn { get; set; } = DateTime.Now;

        public int? BookId { get; set; }

        public int? AppUserId { get; set; }

        public virtual AppUser AppUser { get; set; } 

        public virtual Book Book { get; set; }
    }
}
