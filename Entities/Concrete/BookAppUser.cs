using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class BookAppUser : IEntity
    {
        public int BookId { get; set; }

        public virtual Book Book { get; set; }

        public int AppUserId { get; set; }

        public virtual AppUser AppUser { get; set; }
    }
}
