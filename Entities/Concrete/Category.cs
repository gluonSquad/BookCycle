using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entities.Concrete
{
    public class Category : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Book> Books{ get; set; }

    }
}
