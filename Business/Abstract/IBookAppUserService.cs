﻿using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IBookAppUserService
    {
        List<Book> GetByAppUserId(int appUserId);
    }
}