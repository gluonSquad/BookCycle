using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IQuotationService
    {
        public void Add(Quotation quotation);
    }
}
