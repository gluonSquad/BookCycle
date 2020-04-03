using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entities.Concrete
{
    public class User : IEntity
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string ProfileImageFile { get; set; }

        public string Description { get; set; }

        public DateTime? CreatedOn { get; set; }

        public bool IsDeleted { get; set; }
        
        public Guid ActivateGuid { get; set; }

        public bool AccountStatus { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public int Age { get; set; }


        public ICollection<Review> Reviews { get; set; }

        public ICollection<Quotation> Quotations { get; set; }



    }
}
