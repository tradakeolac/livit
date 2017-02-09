using System;

namespace Livit.Service.Exceptions
{
    public class UpdateActionException : BusinessException
    {
        public UpdateActionException(string message, Exception inner) : base(message, inner)
        {
        }

        public override int Code
        {
            get { return (int)ErrorCode.Update; }
        }
    }
}