using System;

namespace Livit.Service.Exceptions
{
    public class MappingException : BusinessException
    {
        public MappingException(string message, Exception inner) : base(message, inner)
        {
        }

        public override int Code
        {
            get
            {
                return (int)ErrorCode.Mapping;
            }
        }
    }
}