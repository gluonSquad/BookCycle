using System;
using System.Collections.Generic;
using System.Text;

namespace BookCycle.Core.Utilities.Results
{
    public class ErrorResult : Result
    {
        public ErrorResult(bool success, string message) : base(false, message)
        {
        }

        public ErrorResult() : base(false)
        {
        }
    }
}
