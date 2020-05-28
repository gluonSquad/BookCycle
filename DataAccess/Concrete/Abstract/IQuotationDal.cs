using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IQuotationDal : IEntityRepository<Quotation>
    {
        public List<Quotation> GetQuotations(out int totalPage, string searchWord, int currentPage);
        public List<Quotation> GetQuotations();
    }
}
