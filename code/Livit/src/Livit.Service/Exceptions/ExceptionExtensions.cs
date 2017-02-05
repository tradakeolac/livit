using System;

namespace Livit.Service.Exceptions
{
    public static class ExceptionExtensions
    {
        public static BusinessException ToBusinessException(this Exception exception)
        {
            return new UnKnowBusinessException("Internal server error occurred");
        }
    }
}