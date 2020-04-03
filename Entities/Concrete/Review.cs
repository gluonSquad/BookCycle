﻿using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entities.Concrete
{
    public class Review : IEntity
    {
        public int Id { get; set; }

        public string HeadLine { get; set; }

        public string ReviewText { get; set; }

        public string BookStatus { get; set; }

        public int ReviewLike { get; set; }

        public  int Rating { get; set; }

        public DateTime? CreatedOn { get; set; }

        public virtual User User { get; set; }

        public virtual Book Book { get; set; }
    }
}