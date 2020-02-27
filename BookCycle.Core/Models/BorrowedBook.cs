using System;
using System.Collections.Generic;
using System.Text;

namespace BookCycle.Core.Models
{
    public class BorrowedBook:BaseModel
    {
        public int BookId { get; set; }
        public int UserId { get; set; }

        public DateTime BorrowingDate { get; set; }

        public DateTime BroughtDate { get; set; }

    }
}
