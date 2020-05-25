using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class QuotationManager : IQuotationService
    {
        private IQuotationDal _quotationDal;

        public QuotationManager(IQuotationDal quotationDal)
        {
            _quotationDal = quotationDal;
        }


        public void Add(Quotation quotation)
        {
            _quotationDal.Add(quotation);
        }
    }
}
