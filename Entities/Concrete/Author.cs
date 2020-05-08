using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Author:IEntity
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public  ICollection<Book> Books{ get; set; }
    }
}
