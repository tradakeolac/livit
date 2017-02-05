namespace Livit.Service.Exceptions
{

    public class CommonException : BusinessException
    {
        public CommonException(string message) : base(message)
        {
        }

        public override int Code
        {
            get
            {
                return (int)ErrorCode.Common;
            }
        }
    }
}