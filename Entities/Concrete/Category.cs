using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entities.Concrete
{
    public class Category : IEntity
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public  ICollection<Book> Books{ get; set; }

    }
}
