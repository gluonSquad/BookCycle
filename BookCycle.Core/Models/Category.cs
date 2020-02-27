using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BookCycle.Core.Models
{
    public class Category : BaseModel
    {
        public Category()
        {
            Books = new Collection<Book>( );
        }
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
