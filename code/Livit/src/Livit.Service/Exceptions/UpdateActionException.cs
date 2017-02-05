namespace Livit.Service.Exceptions
{
    public class UpdateActionException : BusinessException
    {
        public UpdateActionException(string message) : base(message)
        {
        }

        public override int Code
        {
            get { return (int)ErrorCode.Update; }
        }
    }
}