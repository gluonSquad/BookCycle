﻿using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        public void Add(Category category);
        public Category ExitsCategory(string categoryName);

    }
}
