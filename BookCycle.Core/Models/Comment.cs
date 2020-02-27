using System;

namespace BookCycle.Core.Models
{
    public class Comment:BaseModel
    {

        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }
        public virtual Book Book { get; set; }
        public virtual User Owner { get; set; }
    }
}
