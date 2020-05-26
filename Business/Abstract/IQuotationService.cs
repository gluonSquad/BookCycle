using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IQuotationService
    {
        public void Add(Quotation quotation);
        public List<Quotation> GetQuotations(out int totalPage, string searchWord, int currentPage);
    }
}
