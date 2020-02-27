using System.Collections.Generic;

namespace BookCycle.Core.Models
{
    public class Author : BaseModel
    {
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}