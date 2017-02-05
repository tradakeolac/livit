using System;

namespace Livit.Service.Exceptions
{
    public class AddActionException : BusinessException
    {
        public AddActionException(string message) : base(message)
        {
        }

        public override int Code
        {
            get { return (int)ErrorCode.Add; }
        }
    }

    public class GetActionException : BusinessException
    {
        public GetActionException(string message) : base(message)
        {
        }

        public override int Code
        {
            get
            {
                return (int)ErrorCode.Get;
            }
        }
    }
}