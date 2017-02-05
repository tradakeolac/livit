namespace Livit.Service.Exceptions
{
    public class DeleteActionException : BusinessException
    {
        public DeleteActionException(string message) : base(message)
        {
        }

        public override int Code
        {
            get { return (int)ErrorCode.Delete; }
        }
    }
}