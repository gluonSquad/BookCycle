using System;
using System.Collections.Generic;

namespace BookCycle.Core.Models
{
    public class User:BaseModel
    {
        public string Name { get; set; }
        public string LastName{ get; set; }
        public string UserName{ get; set; }

        public string Mail { get; set; }

        public string Password { get; set; }

        public string Telephone { get; set; }
        public string ProfileImageFile { get; set; }

        public Guid ActivateGuid { get; set; }

        public bool IsAdmin { get; set; }


        public int Penal { get; set; }


       
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Liked> Likes { get; set; }

        public virtual ICollection<BorrowedBook> BorrowedBooks { get; set; }


    }
}
