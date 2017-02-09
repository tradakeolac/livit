using System;

namespace Livit.Service.Exceptions
{
    public class UnKnowBusinessException : BusinessException
    {
        public UnKnowBusinessException(string message, Exception inner) : base(message, inner)
        {
        }

        public override int Code
        {
            get { return (int)ErrorCode.Unknow; }
        }
    }
}