using System;

namespace Livit.Service.Exceptions
{
    public class AddActionException : BusinessException
    {
        public AddActionException(string message, Exception inner) : base(message, inner)
        {
        }

        public override int Code
        {
            get { return (int)ErrorCode.Add; }
        }
    }
}