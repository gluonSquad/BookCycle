using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Entities.Concrete
{
    public class AppUser : IdentityUser<int> , IEntity
    {
        public AppUser()
        {
            Messages = new HashSet<Message>();
        }
        public string ProfileImageFile { get; set; } = "default.png";

        public string Description { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Age { get; set; }


        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<BookAppUser> BookAppUsers { get; set; }
        public virtual ICollection<Quotation> Quotations { get; set; }
    }
}
