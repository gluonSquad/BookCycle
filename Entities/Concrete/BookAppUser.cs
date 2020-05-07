using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class BookAppUser
    {
        public int BookId { get; set; }

        public virtual Book Book { get; set; }

        public int AppUserId { get; set; }

        public virtual AppUser AppUser { get; set; }
    }
}
