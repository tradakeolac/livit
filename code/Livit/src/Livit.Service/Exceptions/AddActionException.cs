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
}