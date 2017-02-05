namespace Livit.Service.Exceptions
{
    public class RequestArgumentNullException : BusinessException
    {
        public RequestArgumentNullException(string message) : base(message)
        {
        }

        public override int Code
        {
            get { return (int)ErrorCode.ArgumentNull; }
        }
    }
}