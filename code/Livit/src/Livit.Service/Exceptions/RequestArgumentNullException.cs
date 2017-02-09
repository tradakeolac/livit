using System;

namespace Livit.Service.Exceptions
{
    public class RequestArgumentNullException : BusinessException
    {
        public RequestArgumentNullException(string message, Exception inner) : base(message, inner)
        {
        }

        public override int Code
        {
            get { return (int)ErrorCode.ArgumentNull; }
        }
    }
}