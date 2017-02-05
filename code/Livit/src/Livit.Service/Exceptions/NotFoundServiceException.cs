namespace Livit.Service.Exceptions
{
    public class NotFoundServiceException : BusinessException
    {
        public NotFoundServiceException(string message) : base(message)
        {
        }

        public override int Code
        {
            get { return (int)ErrorCode.NotFoundService; }
        }
    }
}