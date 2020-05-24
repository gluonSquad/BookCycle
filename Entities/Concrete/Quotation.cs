using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entities.Concrete
{
    public class Quotation : IEntity
    {
        public int Id { get; set; }

        public string QuotationText { get; set; }

        public int CurrentPage { get; set; }

        public int QuotesLike { get; set; }

        public DateTime? CreatedOn { get; set; }


        public int? BookId { get; set; }

        public int? AppUserId { get; set; }
        public virtual  AppUser AppUser { get; set; }

        public virtual Book Book { get; set; }

    }
}
