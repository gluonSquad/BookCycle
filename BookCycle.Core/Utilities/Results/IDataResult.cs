using System;
using System.Collections.Generic;
using System.Text;

namespace BookCycle.Core.Utilities.Results
{
    public interface IDataResult<T> : IResult
    {
        T Data { get; }
    }
}
