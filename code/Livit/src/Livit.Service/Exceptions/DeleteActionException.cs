using System;

namespace Livit.Service.Exceptions
{
    public class DeleteActionException : BusinessException
    {
        public DeleteActionException(string message, Exception inner) : base(message, inner)
        {
        }

        public override int Code
        {
            get { return (int)ErrorCode.Delete; }
        }
    }
}