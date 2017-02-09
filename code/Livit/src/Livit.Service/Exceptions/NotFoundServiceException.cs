using System;

namespace Livit.Service.Exceptions
{
    public class NotFoundServiceException : BusinessException
    {
        public NotFoundServiceException(string message, Exception inner) : base(message, inner)
        {
        }

        public override int Code
        {
            get { return (int)ErrorCode.NotFoundService; }
        }
    }
}